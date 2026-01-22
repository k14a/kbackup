using Spectre.Console;
using System.Text.Json;
using KintoneNetLibrary.Backup.Application.Interfaces;
using KBackup.Core.Enums;

namespace KBackup.Cli.Utils;

public static class CommandUtils {
    public static bool ShouldUseColor(ColorMode mode) {
        return mode switch {
            ColorMode.Always => true,
            ColorMode.Never => false,
            ColorMode.Auto => !Console.IsOutputRedirected,
            _ => false
        };
    }

    public static void PrintJson(IOperationResult result, bool? pretty = null, bool? escapeUnicode = null) {
        var json = JsonSerializer.Serialize(
            result,
            new JsonSerializerOptions {
                WriteIndented = pretty ?? false,
                Encoder = escapeUnicode ?? true
                    ? null
                    : System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }
        );

        Console.WriteLine(json);
    }

    public static void PrintResult(IOperationResult result, bool color) {
        if (!color) {
            Console.WriteLine(result.Success
                ? "Backup completed successfully."
                : "Backup failed.");

            if (result.IsPartialSuccess) {
                Console.WriteLine("Backup completed with warnings.");
            }

            foreach (var w in result.Warnings) {
                Console.WriteLine($"WARNING: {w}");
            }

            foreach (var e in result.Errors) {
                Console.WriteLine($"ERROR: {e}");
            }

            return;
        }

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

        foreach (var e in result.Errors) {
            AnsiConsole.MarkupLine($"[red]ERROR:[/] {e}");
        }
    }
}
