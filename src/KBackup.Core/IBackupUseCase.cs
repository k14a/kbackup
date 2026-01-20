namespace KBackup.Core;

public interface IBackupUseCase {
    Task RunBackupAsync(string subDomain, int appId, string apiToken, string? outDir);
}