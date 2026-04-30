using KBackup.Core.Interfaces;
using KBackup.Core.Logging;
using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KBackup.Core;

/// <summary>
/// BackupUseCase クラスは、バックアップ処理を実行するユースケースクラスであり、IBackupUseCase インターフェースを実装しています。このクラスは、バックアップ処理のビジネスロジックを担当し、IKintoneRepository を使用してバックアップ処理を実行します。また、ILogger を使用して、バックアップ処理の開始と終了、および処理結果に応じたログメッセージを出力します。
/// </summary>
/// <param name="repository">バックアップ処理を実行するためのリポジトリインスタンス</param>
/// <param name="logger">ログ出力を行うための ILogger インスタンス</param>
public class BackupUseCase(IBackupKintoneRepository repository, ILogger<BackupUseCase> logger) : IBackupUseCase {

    private readonly IBackupKintoneRepository _repository = repository;
    private readonly ILogger<BackupUseCase> _logger = logger;

    /// <summary>
    /// RunBackupAsync メソッドは、バックアップ処理を非同期に実行するメソッドであり、BackupRequest オブジェクトを引数に取ります。このメソッドは、バックアップ処理の開始と終了、および処理結果に応じたログメッセージを出力します。バックアップ処理自体は、IKintoneRepository の BackupAsync メソッドに委譲されます。処理結果は BackupResult オブジェクトとして返されます。
    /// </summary>
    /// <param name="request">バックアップ処理を実行するためのリクエストオブジェクト</param>
    /// <returns>バックアップ処理の結果を表す BackupResult オブジェクト</returns>
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