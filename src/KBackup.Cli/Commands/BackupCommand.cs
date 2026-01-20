using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using KBackup.Core;
using KintoneNetLibrary.Backup.Application.DTOs;
using Spectre.Console;
using KBackup.Core.Enums;
using System.Text.Json;
using KBackup.Cli.Config;

namespace KBackup.Cli.Commands;

public class BackupCommand : Command {
    public BackupCommand(IHost host) : base("backup", "Perform backup of kintone application data") {
        // Define options
        var subDomainOption = new Option<string>("--sub-domain") {
            Required = true,
            Description = "Kintone sub-domain"
        };

        var appIdOption = new Option<int>("--app-id") {
            Required = true,
            Description = "Kintone application ID"
        };

        var apiToken = new Option<string>("--api-token") {
            Required = false,
            Description = "Kintone API token"
        };

        var outDirOption = new Option<string?>("--out-dir") {
            Description = "Output directory for backup files"
        };

        var colorOption = new Option<ColorMode>("--color") {
            Description = "Enable colored output",
            DefaultValueFactory = _ => ColorMode.Auto
        };

        var jsonOption = new Option<bool>("--json") {
            Description = "Output result as JSON"
        };

        var configOption = new Option<string?>("--config") {
            Description = "Path to kbackup.config.json"
        };

        // Add options to the command
        this.Add(subDomainOption);
        this.Add(appIdOption);
        this.Add(apiToken);
        this.Add(outDirOption);
        this.Add(colorOption);
        this.Add(jsonOption);
        this.Add(configOption);

        this.SetAction(async parseResult => {
            var configPath = parseResult.GetValue(configOption);
            var config = ConfigLoader.Load(configPath);

            var subDomain = parseResult.GetValue(subDomainOption)
                ?? config?.SubDomain
                ?? throw new ArgumentException("Sub-domain must be specified either via command line or config file.");
            var appId = parseResult.GetValue(appIdOption) != 0 ? config?.AppId
                ?? throw new ArgumentException("App ID must be specified either via command line or config file.");
            var apiTokenValue = parseResult.GetValue(apiToken)
                ?? config?.ApiToken
                ?? throw new ArgumentException("API token must be specified either via command line or config file.");
            var outDir = parseResult.GetValue(outDirOption)
                ?? config?.OutputDirectory
                ?? ".";

            var colorMode = parseResult.GetValue(colorOption);
            var jsonOutput = parseResult.GetValue(jsonOption);

            var logger = host.Services.GetRequiredService<ILoggerFactory>()
                                          .CreateLogger("kbackup");

            var useCase = host.Services.GetRequiredService<IBackupUseCase>();

            var result = await useCase.RunBackupAsync(
                subDomain: subDomain,
                appId: appId,
                apiToken: apiTokenValue,
                outDir: outDir
            );

            // JSON 出力が指定された場合は最優先
            if (jsonOutput) {
                PrintJson(result);
                return;
            }

            // 色付き出力
            var useColor = ShouldUseColor(colorMode);
            PrintResult(result, useColor);

            // Handler logic goes here
            Console.WriteLine($"Backing up app {appId} from {subDomain} to {outDir}");
        });
    }
    private static bool ShouldUseColor(ColorMode mode) {
        return mode switch {
            ColorMode.Always => true,
            ColorMode.Never => false,
            ColorMode.Auto => !Console.IsOutputRedirected,
            _ => false
        };
    }
    private static void PrintResult(BackupResult result, bool color) {
        if (!color) {
            // 色なしの標準出力
            Console.WriteLine(result.Success
                ? "Backup completed successfully."
                : "Backup failed.");

            if (result.IsPartialSuccess) {
                Console.WriteLine("Backup completed with warnings.");
            }

            foreach (var w in result.Warnings) {
                Console.WriteLine($"WARNING: {w}");
            }

            foreach (var s in result.Skipped) {
                Console.WriteLine($"SKIPPED: {s}");
            }

            foreach (var e in result.ErrorMessages) {
                Console.WriteLine($"ERROR: {e}");
            }

            return;
        }

        // 色付き出力（Spectre.Console）
        if (!result.Success) {
            AnsiConsole.MarkupLine("[red]Backup failed[/]");
        } else if (result.IsPartialSuccess) {
            AnsiConsole.MarkupLine("[yellow]Backup completed with warnings[/]");
        } else {
            AnsiConsole.MarkupLine("[green]Backup completed successfully[/]");
        }

        foreach (var w in result.Warnings) {
            AnsiConsole.MarkupLine($"[yellow]WARNING:[/] {w}");
        }

        foreach (var s in result.Skipped) {
            AnsiConsole.MarkupLine($"[blue]SKIPPED:[/] {s}");
        }

        foreach (var e in result.ErrorMessages) {
            AnsiConsole.MarkupLine($"[red]ERROR:[/] {e}");
        }
    }
    private static void PrintJson(BackupResult result) {
        var json = JsonSerializer.Serialize(
            result,
            new JsonSerializerOptions {
                WriteIndented = true
            }
        );

        Console.WriteLine(json);
    }
}