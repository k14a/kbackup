using System.CommandLine;
using KBackup.Core.Enums;
using KintoneNetLibrary.Backup.Domain.Enums;
using KintoneNetLibrary.Domain.Common;

namespace KBackup.Cli.Factories;

public static class OptionFactory {
    public static readonly Option<string> SubDomain =
        new("--sub-domain") {
            Required = true,
            Description = "Kintone sub-domain"
        };
    public static readonly Option<int> AppId =
        new("--app-id") {
            Required = true,
            Description = "Kintone application ID."
        };
    public static readonly Option<string> ApiToken =
        new("--api-token") {
            Required = false,
            Description = "Kintone API token"
        };
    public static readonly Option<ColorMode> OutputColorMode =
        new("--color-mode") {
            Description = "Color output mode (Auto, Always, Never). Default is Auto.",
            DefaultValueFactory = _ => ColorMode.Auto
        };
    public static readonly Option<bool> JsonOutput =
        new("--json") {
            Description = "Output the result in JSON format. Default is false.",
            DefaultValueFactory = _ => false
        };

    #region <<for BackupCommand>>
    public static readonly Option<string> OutputDir =
        new("--out-dir") {
            Description = "Parent directory where backup folders will be created.",
            DefaultValueFactory = _ => "."
        };
    public static readonly Option<bool> DownloadFiles =
        new("--download-files") {
            Description = "Whether to download attached files (if any). Default is true.",
            DefaultValueFactory = _ => true
        };
    public static readonly Option<bool> IncludeFieldSchema =
        new("--include-field-schema") {
            Description = "Whether to include field schema in the backup. Default is true.",
            DefaultValueFactory = _ => true
        };
    public static readonly Option<bool> Overwrite =
        new("--overwrite") {
            Description = "Whether to overwrite existing files in the backup directory. Default is false. Use with caution.",
            DefaultValueFactory = _ => false
        };
    public static readonly Option<bool> Pretty =
        new("--pretty") {
            Description = "Whether to pretty-print JSON output. Default is false.",
            DefaultValueFactory = _ => false
        };
    public static readonly Option<bool> EscapeUnicode =
        new("--escape") {
            Description = "Whether to escape unicode characters in JSON output. Default is true.",
            DefaultValueFactory = _ => true
        };
    public static readonly Option<string?> Query =
        new("--query") {
            Description = "Query expression to filter records for backup."
        };
    public static readonly Option<int> Split =
        new("--split") {
            Description = "Number of records per split file. If not specified, all records are saved in a single file. Default is 1000. Maximum is 5000.",
            DefaultValueFactory = _ => 1000
        };
    #endregion

    #region <<for RestoreCommand>>
    public static readonly Option<string?> BackupDir =
        new("--backup-dir") {
            Description = "Root directory of the backup (e.g., backup/AppID000001/20260101_123456)"
        };
    public static readonly Option<RestoreMode> Mode =
        new("--mode") {
            Description = "Restore mode (FullReplace, Upsert, Merge). Default is Upsert",
            DefaultValueFactory = _ => RestoreMode.Upsert
        };
    public static readonly Option<bool> Force =
        new("--force") {
            Description = "Skip schema validation and allow dangerous operations. Use with caution.",
            DefaultValueFactory = _ => false
        };
    public static readonly Option<bool> RestoreFiles =
        new("--restore-files") {
            Description = "Restore attached files (if any)",
            DefaultValueFactory = _ => true
        };
    public static readonly Option<bool> DryRun =
        new("--dry-run") {
            Description = "Perform a dry run without making actual changes",
            DefaultValueFactory = _ => false
        };
    public static readonly Option<bool> ValidateOnly =
        new("--validate-only") {
            Description = "Only validate the restore request without executing",
            DefaultValueFactory = _ => false
        };
    public static readonly Option<int> BatchSize =
        new("--batch-size") {
            Description = "Number of records to process in each batch",
            DefaultValueFactory = _ => KintoneConstants.KintoneLimit
        };
    #endregion
}
