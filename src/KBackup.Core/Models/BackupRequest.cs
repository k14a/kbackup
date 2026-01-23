namespace KBackup.Core.Models;

public sealed class BackupRequest : BaseKintoneRequest {
    public required DirectoryInfo OutputDirectory { get; init; }

    // BackupOptions に対応する項目
    public bool DownloadFiles { get; init; } = true;
    public bool IncludeFieldSchema { get; init; } = true;
    public bool Overwrite { get; init; } = false;
    public bool Pretty { get; init; } = false;
    public bool EscapeUnicode { get; init; } = true;
    public string? Query { get; init; } = null;
    public int SplitSize { get; init; } = 1000;
}
