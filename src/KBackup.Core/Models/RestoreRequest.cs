using KintoneNetLibrary.Backup.Domain.Enums;

namespace KBackup.Core.Models;

public sealed class RestoreRequest : BaseKintoneRequest {
    public required DirectoryInfo BackupDirectory { get; init; }

    // RestoreOptions に対応する項目
    public RestoreMode Mode { get; init; } = RestoreMode.Upsert;
    public bool RestoreFiles { get; init; } = true;
    public bool Force { get; init; } = false;
}
