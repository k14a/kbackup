using System.CommandLine;
using KBackup.Cli.Utils;
using KBackup.Core.Interfaces;
using KBackup.Core.Logging;
using KintoneNetLibrary.Backup.Domain.Enums;
using KintoneNetLibrary.Domain.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KBackup.Cli.Commands;

public class RestoreCommand : BaseCommand {
    public RestoreCommand(IHost host, ILogger<RestoreCommand> logger)
        : base("restore", "Restore Kintone application data from backup", logger) {

        StaticLogMessages.MethodStart(this._logger);

        try {
            var subDomainOption = new Option<string>("--sub-domain") {
                Description = "Kintone sub-domain"
            };
            var appIdOption = new Option<int>("--app-id") {
                Description = "Kintone application ID"
            };
            var apiTokenOption = new Option<string>("--api-token") {
                Description = "Kintone API token"
            };
            var backupDirOption = new Option<string?>("--backup-dir") {
                Description = "Root directory of the backup (e.g., backup/AppID000001/20260101_123456)"
            };
            var modeOption = new Option<RestoreMode?>("--mode") {
                Description = "Restore mode (FullReplace, Upsert, Merge). Default is Upsert"
            };
            var forceOption = new Option<bool?>("--force") {
                Description = "Skip schema validation and allow dangerous operations. Use with caution."
            };
            var restoreFilesOption = new Option<bool?>("--restore-files") {
                Description = "Restore attached files (if any)"
            };
            var dryRunOption = new Option<bool?>("--dry-run") {
                Description = "Perform a dry run without making actual changes"
            };
            var validateOnlyOption = new Option<bool?>("--validate-only") {
                Description = "Only validate the restore request without executing"
            };
            var batchSizeOption = new Option<int?>("--batch-size") {
                Description = "Number of records to process in each batch"
            };

            this.Add(subDomainOption);
            this.Add(appIdOption);
            this.Add(apiTokenOption);
            this.Add(backupDirOption);
            this.Add(modeOption);
            this.Add(forceOption);
            this.Add(restoreFilesOption);
            this.Add(dryRunOption);
            this.Add(validateOnlyOption);
            this.Add(batchSizeOption);
            this.SetAction(async parseResult => {
                var config = this.LoadConfig(parseResult);

                var subDomain = parseResult.GetValue(subDomainOption)
                    ?? config?.SubDomain
                    ?? throw new ArgumentException("Sub-domain must be specified.");
                var appId = parseResult.GetValue(appIdOption) != 0
                    ? parseResult.GetValue(appIdOption)
                    : config?.AppId
                        ?? throw new ArgumentException("App ID must be specified.");
                var apiToken = parseResult.GetValue(apiTokenOption)
                    ?? config?.ApiToken
                    ?? throw new ArgumentException("API token must be specified.");
                var backupDir = parseResult.GetValue(backupDirOption)
                    ?? config?.OutputDirectory
                    ?? throw new ArgumentException("Backup directory must be specified.");
                var mode = parseResult.GetValue(modeOption)
                    ?? config?.Mode
                    ?? RestoreMode.Upsert;
                var force = parseResult.GetValue(forceOption)
                    ?? config?.Force
                    ?? false;
                var restoreFiles = parseResult.GetValue(restoreFilesOption)
                    ?? config?.RestoreFiles
                    ?? true;
                var dryRun = parseResult.GetValue(dryRunOption)
                    ?? config?.DryRun
                    ?? false;
                var validateOnly = parseResult.GetValue(validateOnlyOption)
                    ?? config?.ValidateOnly
                    ?? false;
                var batchSize = parseResult.GetValue(batchSizeOption)
                    ?? config?.BatchSize
                    ?? KintoneConstants.KintoneLimit;

                var colorMode = parseResult.GetValue(this.ColorOption);
                var jsonOutput = parseResult.GetValue(this.JsonOption);

                var backupDirInfo = new DirectoryInfo(backupDir);
                if (!backupDirInfo.Exists) {
                    throw new DirectoryNotFoundException($"Backup directory not found: {backupDirInfo.FullName}");
                }

                var request = new Core.Models.RestoreRequest {
                    SubDomain = subDomain,
                    AppId = appId,
                    ApiToken = apiToken,
                    BackupDirectory = backupDirInfo,
                    Mode = mode,
                    RestoreFiles = restoreFiles,
                    Force = force
                };
                var useCase = host.Services.GetRequiredService<IRestoreUseCase>();
                var result = await useCase.RunRestoreAsync(request);

                if (jsonOutput) {
                    CommandUtils.PrintJson(result);
                    return;
                }

                var useColor = CommandUtils.ShouldUseColor(colorMode);
                CommandUtils.PrintResult(result, useColor);
            });

        } finally {
            StaticLogMessages.MethodFinish(this._logger);
        }
    }
}
