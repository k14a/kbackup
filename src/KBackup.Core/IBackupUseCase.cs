using KintoneNetLibrary.Backup.Application.DTOs;

namespace KBackup.Core;

public interface IBackupUseCase {
    Task<BackupResult> RunBackupAsync(string subDomain, int appId, string apiToken, string? outDir);
}