using KBackup.Core.Enums;
using KintoneNetLibrary.Backup.Domain.Enums;

namespace KBackup.Cli.Options;

public sealed class RestoreOptions {
    // BaseKintoneRequest に対応
    public required string SubDomain { get; init; }
    public required int AppId { get; init; }
    public required string ApiToken { get; init; }

    // RestoreRequest に対応
    public required string BackupDir { get; init; }
    public RestoreMode Mode { get; init; }
    public bool RestoreFiles { get; init; }
    public bool Force { get; init; }
    public bool DryRun { get; init; }
    public bool ValidateOnly { get; init; }
    public int BatchSize { get; init; }

    // 出力設定
    public ColorMode ColorMode { get; init; }
    public bool JsonOutput { get; init; }
}