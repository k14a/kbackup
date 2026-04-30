using KBackup.Core.Interfaces;
using KBackup.Core.Logging;
using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;

namespace KBackup.Infrastructure;

/// <summary>
/// KintoneRepository クラスは、IKintoneRepository インターフェースを実装するクラスであり、Kintone のバックアップおよびリストア処理を実行するためのリポジトリクラスです。このクラスは、KintoneBackupService を使用してバックアップおよびリストア処理を実行し、ILogger を使用して処理の開始と終了、および処理結果に応じたログメッセージを出力します。
/// </summary>
public partial class BackupKintoneRepository : IBackupKintoneRepository {
    /// <summary>
    /// BackupAsync メソッドは、Kintone のバックアップ処理を非同期に実行するメソッドであり、BackupRequest オブジェクトを引数に取ります。このメソッドは、バックアップ処理の開始と終了、および処理結果に応じたログメッセージを出力します。バックアップ処理自体は、KintoneBackupService の RunBackupAsync メソッドに委譲されます。処理結果は BackupResult オブジェクトとして返されます。
    /// </summary>
    /// <param name="request">バックアップ処理のリクエスト情報を含む BackupRequest オブジェクト</param>
    /// <returns>バックアップ処理の結果を含む BackupResult オブジェクト</returns>
    public async Task<BackupResult> BackupAsync(BackupRequest request) {
        StaticLogMessages.MethodStart(this._logger, message: $"KintoneRepository.RunBackupAsync started. App={request.AppId}");

        try {
            this._backupService.Options = new BackupOptions {
                SubDomain = request.SubDomain,
                AppID = request.AppId,
                ApiToken = request.ApiToken,
                OutputPath = request.OutputDirectory,
                DownloadFiles = request.DownloadFiles,
                IncludeFieldSchema = request.IncludeFieldSchema,
                Overwrite = request.Overwrite,
                Pretty = request.Pretty,
                EscapeUnicode = request.EscapeUnicode,
                Query = request.Query,
                SplitSize = request.SplitSize
            };

            return await this._backupService.RunBackupAsync();

        } finally {
            StaticLogMessages.MethodFinish(this._logger, message: $"KintoneRepository.RunBackupAsync finished. App={request.AppId}");
        }
    }
}
