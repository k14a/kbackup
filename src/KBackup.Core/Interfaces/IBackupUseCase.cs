using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;

namespace KBackup.Core.Interfaces;

public interface IBackupUseCase {
    Task<BackupResult> RunBackupAsync(BackupRequest request);
}