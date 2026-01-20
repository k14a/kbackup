using KBackup.Core;
using KintoneNetLibrary;
using KintoneNetLibrary.Backup;
using KintoneNetLibrary.Backup.Application.DTOs;
using KintoneNetLibrary.Backup.Application.Interfaces;
using KintoneNetLibrary.CodeGen.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace KBackup.Infrastructure;

public class KintoneRepository(
    IBackupService backupService,
    IHttpClientFactory httpClientFactory,
    ISchemaProvider schemaProvider,
    ILogger<KintoneRepository> logger) : IKintoneRepository {
    private readonly IBackupService _backupService = backupService;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ISchemaProvider _schemaProvider = schemaProvider;
    private readonly ILogger<KintoneRepository> _logger = logger;

    public async Task<BackupResult> RunBackupAsync(
        string subDomain,
        int appId,
        string apiToken,
        string? outDir) {
        var service = new BackupService(
            new BackupOptions {
                SubDomain = subDomain,
                AppID = appId,
                ApiToken = apiToken,
                OutputPath = BuildOutputPath(outDir, appId),
                DownloadFiles = true,
                IncludeFieldSchema = true,
                Overwrite = false
            },
            this._schemaProvider,
            _httpClient,
            this._logger
        );

        return await service.RunBackupAsync();
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
