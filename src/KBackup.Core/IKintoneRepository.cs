namespace KBackup.Core;

public interface IKintoneRepository {
    Task<KintoneBackupData> GetAppBackupDataAsync(int appId);

    Task SaveBackupAsync(int appId, KintoneBackupData data, string outputDirectory);
}
