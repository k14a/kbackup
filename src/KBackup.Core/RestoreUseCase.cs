using KBackup.Core.Interfaces;
using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KBackup.Core;

/// <summary>
/// RestoreUseCase クラスは、リストア処理を実行するユースケースクラスであり、IRestoreUseCase インターフェースを実装しています。このクラスは、リストア処理のビジネスロジックを担当し、IKintoneRepository を使用してリストア処理を実行します。また、ILogger を使用して、リストア処理の開始と終了、および処理結果に応じたログメッセージを出力します。
/// </summary>
/// <param name="repository">リストア処理を実行するための Kintone リポジトリ</param>
/// <param name="logger">ログ出力を行うための ILogger インスタンス</param>
public class RestoreUseCase(IKintoneRepository repository, ILogger<RestoreUseCase> logger) : IRestoreUseCase {

    private readonly IKintoneRepository _repository = repository;
    private readonly ILogger<RestoreUseCase> _logger = logger;

    /// <summary>
    /// RunRestoreAsync メソッドは、リストア処理を非同期に実行するメソッドであり、RestoreRequest オブジェクトを引数に取ります。このメソッドは、リストア処理の開始と終了、および処理結果に応じたログメッセージを出力します。リストア処理自体は、IKintoneRepository の RestoreAsync メソッドに委譲されます。処理結果は RestoreResult オブジェクトとして返されます。
    /// </summary>
    /// <param name="request">リストア処理のリクエスト情報を含む RestoreRequest オブジェクト</param>
    /// <returns>リストア処理の結果を含む RestoreResult オブジェクト</returns>
    public async Task<RestoreResult> RunRestoreAsync(RestoreRequest request) {
        this._logger.LogInformation("Restore started. App={AppId}", request.AppId);

        var result = await this._repository.RestoreAsync(request);

        if (!result.Success) {
            this._logger.LogWarning("Restore finished with errors.");
        } else if (result.IsPartialSuccess) {
            this._logger.LogWarning("Restore finished with warnings.");
        } else {
            this._logger.LogInformation("Restore finished successfully.");
        }

        return result;
    }
}
