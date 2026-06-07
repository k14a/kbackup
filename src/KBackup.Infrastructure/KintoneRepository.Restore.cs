using KBackup.Core.Enums;
using KBackup.Core.Logging;
using KBackup.Core.Models;
using KintoneRestoreMode = KintoneNetLibrary.Backup.Domain.Enums.RestoreMode;
using KintoneRestoreOptions = KintoneNetLibrary.Backup.Application.DTOs.RestoreOptions;
using KintoneRestoreResult = KintoneNetLibrary.Backup.Application.DTOs.RestoreResult;

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

        try {
            var options = new KintoneRestoreOptions {
                SubDomain = request.SubDomain,
                AppId = request.AppId,
                ApiToken = request.ApiToken,
                BackupRootPath = request.BackupDirectory,
                Mode = MapRestoreMode(request.Mode),
                RestoreFiles = request.RestoreFiles,
                Force = request.Force,
                DryRun = request.DryRun,
                ValidateOnly = request.ValidateOnly,
                BatchSize = request.BatchSize
            };

            var kintoneResult = await this._restoreService.RunRestoreAsync(options);
            return MapRestoreResult(kintoneResult);

        } catch (Exception ex) {
            var errorResult = new RestoreResult { Success = false };
            errorResult.Errors.Add(ex.Message);
            StaticLogMessages.MethodError(this._logger, ex, message: $"KintoneRepository.RestoreAsync failed. App={request.AppId}");
            return errorResult;

        } finally {
            StaticLogMessages.MethodFinish(this._logger, message: $"KintoneRepository.RestoreAsync finished. App={request.AppId}");
        }
    }

    /// <summary>
    /// Core の RestoreMode を KintoneNetLibrary の RestoreMode に変換する。
    /// </summary>
    private static KintoneRestoreMode MapRestoreMode(RestoreMode mode) => mode switch {
        RestoreMode.FullReplace => KintoneRestoreMode.FullReplace,
        RestoreMode.Upsert => KintoneRestoreMode.Upsert,
        RestoreMode.Merge => KintoneRestoreMode.Merge,
        _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
    };

    /// <summary>
    /// KintoneNetLibrary の RestoreResult を Core の RestoreResult にマッピングする。
    /// </summary>
    private static RestoreResult MapRestoreResult(KintoneRestoreResult r) => new() {
        Success = r.Success,
        AddedRecords = r.AddedRecords,
        UpdatedRecords = r.UpdatedRecords,
        DeletedRecords = r.DeletedRecords,
        UploadedFiles = r.UploadedFiles,
        FailedFiles = r.FailedFiles,
        Warnings = r.Warnings,
        Errors = r.Errors
    };
}
