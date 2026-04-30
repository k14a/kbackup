using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;

namespace KBackup.Core.Interfaces;

/// <summary>
/// IBackupKintoneRepository インターフェースは、Kintone へのバックアップとリストアの操作を定義するインターフェースです。BackupAsync メソッドはバックアップ処理を実行し、RestoreAsync メソッドはリストア処理を実行します。両方のメソッドは非同期で実行され、結果を表すオブジェクトを返します。
/// </summary>
public interface IBackupKintoneRepository {
    /// <summary>
    /// BackupAsync メソッドは、指定された BackupRequest を使用してバックアップを実行し、BackupResult を返します。
    /// </summary>
    /// <param name="request">バックアップリクエストを表す BackupRequest オブジェクト。</param>
    /// <returns>バックアップ結果を表す BackupResult オブジェクト。</returns>
    Task<BackupResult> BackupAsync(BackupRequest request);

    /// <summary>
    /// RestoreAsync メソッドは、指定された RestoreRequest を使用してリストアを実行し、RestoreResult を返します。
    /// </summary>
    /// <param name="request">リストアリクエストを表す RestoreRequest オブジェクト。</param>
    /// <returns>リストア結果を表す RestoreResult オブジェクト。</returns>
    Task<RestoreResult> RestoreAsync(RestoreRequest request);
}
