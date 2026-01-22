using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;

namespace KBackup.Core.Interfaces;

public interface IKintoneRepository {
    Task<BackupResult> BackupAsync(BackupRequest request);

    Task<RestoreResult> RestoreAsync(RestoreRequest request);
}
