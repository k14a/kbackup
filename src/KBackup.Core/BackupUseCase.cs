using KintoneNetLibrary.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace KBackup.Core;

public class BackupUseCase : IBackupUseCase {
    private readonly IKintoneRepository _repository;
    private readonly ILogger<BackupUseCase> _logger;

    public BackupUseCase(
        IKintoneRepository repository,
        ILogger<BackupUseCase> logger) {

        this._repository = repository;
        this._logger = logger;
    }

    public async Task RunBackupAsync(string subDomain, int appId, string apiToken, string? outDir) {
        this._logger.LogInformation("BackupUseCase started. App={AppId}", appId);

        // 1. データ取得（スキーマ・レコード・添付ファイルなど）
        var data = await this._repository.GetAppBackupDataAsync(appId);

        // 2. 保存（Infrastructure 側に委譲）
        await this._repository.SaveBackupAsync(appId, data, outDir);

        this._logger.LogInformation("BackupUseCase finished. App={AppId}", appId);
    }
}
