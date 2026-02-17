using KBackup.Core.Logging;
using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;

namespace KBackup.Infrastructure;

/// <summary>
/// KintoneRepository クラスは、IKintoneRepository インターフェースを実装するクラスであり、Kintone のバックアップおよびリストア処理を実行するためのリポジトリクラスです。このクラスは、KintoneBackupService を使用してバックアップおよびリストア処理を実行し、ILogger を使用して処理の開始と終了、および処理結果に応じたログメッセージを出力します。
/// </summary>
public partial class KintoneRepository {
    /// <summary>
    /// RestoreAsync メソッドは、Kintone のリストア処理を非同期に実行するメソッドであり、RestoreRequest オブジェクトを引数に取ります。このメソッドは、リストア処理の開始と終了、および処理結果に応じたログメッセージを出力します。リストア処理自体は、KintoneRestoreService の RunRestoreAsync メソッドに委譲されます。処理結果は RestoreResult オブジェクトとして返されます。
    /// </summary>
    /// <param name="request">リストア処理のリクエスト情報を含む RestoreRequest オブジェクト</param>
    /// <returns>リストア処理の結果を含む RestoreResult オブジェクト</returns>
    public async Task<RestoreResult> RestoreAsync(RestoreRequest request) {
        StaticLogMessages.MethodStart(this._logger, message: $"KintoneRepository.RestoreAsync started. App={request.AppId}");

        var result = new RestoreResult();

        try {
            // RestoreService のオプションをセット
            var options = new RestoreOptions {
                SubDomain = request.SubDomain,
                AppID = request.AppId,
                ApiToken = request.ApiToken,
                BackupRootPath = request.BackupDirectory,
                Mode = request.Mode,
                RestoreFiles = request.RestoreFiles,
                Force = request.Force,
                DryRun = request.DryRun,
                ValidateOnly = request.ValidateOnly,
                BatchSize = request.BatchSize
            };

            // RestoreService にオプションを注入
            this._restoreService.Options = options;

            return await this._restoreService.RunRestoreAsync();

        } catch (Exception ex) {
            result.Success = false;
            result.Errors.Add(ex.Message);
            StaticLogMessages.MethodError(this._logger, ex, message: $"KintoneRepository.RestoreAsync failed. App={request.AppId}");
            return result;

        } finally {
            StaticLogMessages.MethodFinish(this._logger, message: $"KintoneRepository.RestoreAsync finished. App={request.AppId}");
        }
    }
}
