using KBackup.Core;
using KintoneNetLibrary;
using KintoneNetLibrary.Backup;
using KintoneNetLibrary.Backup.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace KBackup.Infrastructure;

public class KintoneRepository(
    IBackupService backupService,
    ILogger<KintoneRepository> logger) : IKintoneRepository {
    private readonly IBackupService _backupService = backupService;
    private readonly ILogger<KintoneRepository> _logger = logger;

    public async Task<KintoneBackupData> GetAppBackupDataAsync(int appId) {
        this._logger.LogInformation("Fetching Kintone data. App={AppId}", appId);

        var result = await this._backupService.RunBackupAsync();
        if (!result.Success) {
            this._logger.LogError("Failed to fetch Kintone data. App={AppId}, Error={Error}", appId, result.ErrorMessage);
            throw new Exception($"Failed to fetch Kintone data: {result.ErrorMessage}");
        }
        // // --- 1. スキーマ取得（仮） ---
        // var schema = await this._backupService.RunBackupAsync();

        // // --- 2. レコード取得（仮） ---
        // var records = await this._backupService.GetAllRecordsAsync(appId);

        // // --- 3. 添付ファイル取得（仮） ---
        // var files = new List<KintoneFileData>();
        // // ※後で実装

        // return new KintoneBackupData {
        //     AppName = schema.AppName,
        //     Schema = schema,
        //     Records = records,
        //     Files = files
        // };
        return null;
    }

    public async Task SaveBackupAsync(int appId, KintoneBackupData data, string outputDirectory) {
        this._logger.LogInformation("Saving backup. App={AppId}, Dir={Dir}", appId, outputDirectory);

        Directory.CreateDirectory(outputDirectory);

        // --- 1. スキーマ保存（仮） ---
        var schemaPath = Path.Combine(outputDirectory, "schema.json");
        await File.WriteAllTextAsync(schemaPath, System.Text.Json.JsonSerializer.Serialize(data.Schema));

        // --- 2. レコード保存（仮） ---
        var recordsPath = Path.Combine(outputDirectory, "records.json");
        await File.WriteAllTextAsync(recordsPath, System.Text.Json.JsonSerializer.Serialize(data.Records));

        // --- 3. 添付ファイル保存（仮） ---
        foreach (var file in data.Files) {
            var filePath = Path.Combine(outputDirectory, file.FileName);
            await File.WriteAllBytesAsync(filePath, file.Content);
        }

        this._logger.LogInformation("Backup saved. App={AppId}", appId);
    }
}
