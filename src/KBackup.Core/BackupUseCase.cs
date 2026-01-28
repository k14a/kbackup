using KBackup.Core.Interfaces;
using KBackup.Core.Logging;
using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KBackup.Core;

public class BackupUseCase(IKintoneRepository repository, ILogger<BackupUseCase> logger) : IBackupUseCase {

    private readonly IKintoneRepository _repository = repository;
    private readonly ILogger<BackupUseCase> _logger = logger;

    public async Task<BackupResult> RunBackupAsync(BackupRequest request) {
        StaticLogMessages.MethodStart(this._logger, message: $"BackupUseCase started. App={request.AppId}");

        try {
            // Repository にバックアップ処理を委譲
            var result = await this._repository.BackupAsync(request);

            if (!result.Success) {
                this._logger.LogWarning("BackupUseCase finished with errors. App={AppId}", request.AppId);
            } else if (result.IsPartialSuccess) {
                this._logger.LogWarning("BackupUseCase finished with warnings. App={AppId}", request.AppId);
            } else {
                this._logger.LogInformation("BackupUseCase finished successfully. App={AppId}", request.AppId);
            }

            return result;

        } finally {
            StaticLogMessages.MethodFinish(this._logger, message: $"BackupUseCase finished. App={request.AppId}");
        }
    }
}