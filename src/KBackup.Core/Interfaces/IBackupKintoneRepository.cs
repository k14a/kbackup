using KBackup.Core.Models;

namespace KBackup.Core.Interfaces;

/// <summary>
/// Kintone へのバックアップ操作を定義するインターフェース。
/// </summary>
public interface IBackupKintoneRepository {
    /// <summary>
    /// 指定された BackupRequest を使用してバックアップを実行し、BackupResult を返します。
    /// </summary>
    /// <param name="request">バックアップリクエストを表す BackupRequest オブジェクト。</param>
    /// <returns>バックアップ結果を表す BackupResult オブジェクト。</returns>
    Task<BackupResult> BackupAsync(BackupRequest request);
}
