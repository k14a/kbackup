using KintoneNetLibrary.Backup.Domain.Enums;
using KintoneNetLibrary.Domain.Common;

namespace KBackup.Core.Models;

public sealed class RestoreRequest : BaseKintoneRequest {
    public required DirectoryInfo BackupDirectory { get; init; }

    // RestoreOptions に対応する項目
    public RestoreMode Mode { get; init; } = RestoreMode.Upsert;
    public bool RestoreFiles { get; init; } = true;
    public bool Force { get; init; } = false;
    public bool DryRun { get; init; } = false;
    public bool ValidateOnly { get; init; } = false;
    public int BatchSize { get; init; } = KintoneConstants.KintoneLimit;
}
