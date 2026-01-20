using KintoneNetLibrary.Backup.Application.DTOs;

namespace KBackup.Core;

public interface IKintoneRepository {
    Task<BackupResult> RunBackupAsync(string subDomain, int appId, string apiToken, string? outDir);

    Task SaveBackupAsync(int appId, KintoneBackupData data, string outputDirectory);
}
