using System.CommandLine;
using KBackup.Cli.Utils;
using KBackup.Core.Interfaces;
using KBackup.Core.Logging;
using KBackup.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KBackup.Cli.Commands;

public class BackupCommand : BaseCommand {
    public BackupCommand(IHost host, ILogger<BackupCommand> logger)
        : base("backup", "Perform backup of kintone application data", logger) {

        // Define options
        var subDomainOption = new Option<string>("--sub-domain") {
            Required = true,
            Description = "Kintone sub-domain"
        };
        var appIdOption = new Option<int>("--app") {
            Required = true,
            Description = "Kintone application ID."
        };
        var apiToken = new Option<string>("--api-token") {
            Required = false,
            Description = "Kintone API token"
        };
        var outDirOption = new Option<string?>("--out-dir") {
            Description = "Parent directory where backup folders will be created."
        };
        var downloadFilesOption = new Option<bool?>("--download-files") {
            Description = "Whether to download attached files (if any). Default is true."
        };
        var includeFieldSchemaOption = new Option<bool?>("--include-field-schema") {
            Description = "Whether to include field schema in the backup. Default is true."
        };
        var overwriteOption = new Option<bool?>("--overwrite") {
            Description = "Whether to overwrite existing files in the output directory. Default is false. Use with caution."
        };
        var prettyOption = new Option<bool?>("--pretty") {
            Description = "Whether to pretty-print JSON output. Default is false."
        };
        var escapeUnicodeOption = new Option<bool?>("--escape") {
            Description = "Whether to escape unicode characters in JSON output. Default is true."
        };
        var queryOption = new Option<string?>("--query") {
            Description = "Query expression to filter records for backup."
        };
        var splitOption = new Option<int?>("--split") {
            Description = "Number of records per split file. If not specified, all records are saved in a single file. Default is 1000. Maximum is 5000."
        };

        // Add options to the command
        this.Add(subDomainOption);
        this.Add(appIdOption);
        this.Add(apiToken);
        this.Add(outDirOption);
        this.Add(downloadFilesOption);
        this.Add(includeFieldSchemaOption);
        this.Add(overwriteOption);
        this.Add(prettyOption);
        this.Add(escapeUnicodeOption);
        this.Add(queryOption);
        this.Add(splitOption);
        this.SetAction(async parseResult => {
            var config = this.LoadConfig(parseResult);

            var subDomain = parseResult.GetValue(subDomainOption)
                ?? config?.SubDomain
                ?? throw new ArgumentException("Sub-domain must be specified either via command line or config file.");
            var appId = parseResult.GetValue(appIdOption) != 0
                ? parseResult.GetValue(appIdOption)
                : config?.AppId
                    ?? throw new ArgumentException("App ID must be specified either via command line or config file.");
            var apiTokenValue = parseResult.GetValue(apiToken)
                ?? config?.ApiToken
                ?? throw new ArgumentException("API token must be specified either via command line or config file.");
            var outDir = parseResult.GetValue(outDirOption)
                ?? config?.OutputDirectory
                ?? ".";
            var downloadFiles = parseResult.GetValue(downloadFilesOption)
                ?? config?.DownloadFiles
                ?? true;
            var includeFieldSchema = parseResult.GetValue(includeFieldSchemaOption)
                ?? config?.IncludeFieldSchema
                ?? true;
            var overwrite = parseResult.GetValue(overwriteOption)
                ?? config?.Overwrite
                ?? false;
            var pretty = parseResult.GetValue(prettyOption)
            ?? config?.Pretty
            ?? false;
            var escapeUnicode = parseResult.GetValue(escapeUnicodeOption)
                ?? config?.EscapeUnicode
                ?? false;
            var query = parseResult.GetValue(queryOption) ?? null;
            var split = parseResult.GetValue(splitOption) ?? 1000;

            var colorMode = parseResult.GetValue(this.ColorOption);
            var jsonOutput = parseResult.GetValue(this.JsonOption);
            var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("kbackup");

            var request = new BackupRequest {
                SubDomain = subDomain,
                AppId = appId,
                ApiToken = apiTokenValue,
                OutputDirectory = new DirectoryInfo(outDir),
                DownloadFiles = downloadFiles,
                IncludeFieldSchema = includeFieldSchema,
                Overwrite = overwrite,
                Pretty = pretty,
                EscapeUnicode = escapeUnicode,
                Query = query,
                SplitSize = split
            };
            var useCase = host.Services.GetRequiredService<IBackupUseCase>();
            var result = await useCase.RunBackupAsync(request);

            // JSON 出力が指定された場合は最優先
            if (jsonOutput) {
                CommandUtils.PrintJson(result, pretty, escapeUnicode);
                return;
            }

            // 色付き出力
            var useColor = CommandUtils.ShouldUseColor(colorMode);
            CommandUtils.PrintResult(result, useColor);
        });
    }
}