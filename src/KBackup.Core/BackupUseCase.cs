using KintoneNetLibrary.Backup.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KBackup.Core;

public class BackupUseCase : IBackupUseCase {
    private readonly IKintoneRepository _repository;
    private readonly ILogger<BackupUseCase> _logger;

    public BackupUseCase(
        IKintoneRepository repository,
        ILogger<BackupUseCase> logger) {
        _repository = repository;
        _logger = logger;
    }

    public async Task<BackupResult> RunBackupAsync(
        string subDomain,
        int appId,
        string apiToken,
        string? outDir) {
        _logger.LogInformation("BackupUseCase started. App={AppId}", appId);

        // Repository にバックアップ処理を委譲
        var result = await _repository.RunBackupAsync(
            subDomain,
            appId,
            apiToken,
            outDir
        );

        if (!result.Success) {
            _logger.LogWarning("BackupUseCase finished with errors. App={AppId}", appId);
        } else if (result.IsPartialSuccess) {
            _logger.LogWarning("BackupUseCase finished with warnings. App={AppId}", appId);
        } else {
            _logger.LogInformation("BackupUseCase finished successfully. App={AppId}", appId);
        }

        return result;
    }
}