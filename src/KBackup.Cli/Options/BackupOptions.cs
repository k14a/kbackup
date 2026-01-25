using KBackup.Core.Enums;
using KintoneNetLibrary.Backup.Domain.Enums;

namespace KBackup.Cli.Options;

public sealed class BackupOptions {
    // BaseKintoneRequest に対応
    public required string SubDomain { get; init; }
    public required int AppId { get; init; }
    public required string ApiToken { get; init; }

    // BackupRequest に対応
    public required string OutputDir { get; init; }
    public bool DownloadFiles { get; init; }
    public bool IncludeFieldSchema { get; init; }
    public bool Overwrite { get; init; }
    public bool Pretty { get; init; }
    public bool EscapeUnicode { get; init; }
    public string? Query { get; init; }
    public int SplitSize { get; init; }

    // 出力設定
    public ColorMode ColorMode { get; init; }
    public bool JsonOutput { get; init; }
}