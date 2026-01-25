using KBackup.Core.Enums;
using KintoneNetLibrary.Backup.Domain.Enums;
using KintoneNetLibrary.Domain.Common;

namespace KBackup.Core.Models;

public sealed class RestoreRequest : BaseKintoneRequest {
    public required DirectoryInfo BackupDirectory { get; init; }

    // RestoreOptions に対応する項目
    public RestoreMode Mode { get; init; }
    public bool RestoreFiles { get; init; }
    public bool Force { get; init; }
    public bool DryRun { get; init; }
    public bool ValidateOnly { get; init; }
    public int BatchSize { get; init; }
    public ColorMode ColorMode { get; init; }
    public bool JsonOutput { get; init; }
}
