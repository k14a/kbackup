using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;

namespace KBackup.Core.Interfaces;

/// <summary>
/// IBackupUseCase インターフェースは、バックアップ処理のユースケースを定義するインターフェースです。RunBackupAsync メソッドを通じて、バックアップリクエストを受け取り、バックアップ結果を返す非同期メソッドを提供します。
/// </summary>
public interface IBackupUseCase {
    /// <summary>
    /// RunBackupAsync メソッドは、指定された BackupRequest を使用してバックアップを実行し、BackupResult を返します。
    /// </summary>
    /// <param name="request">バックアップリクエストを表す BackupRequest オブジェクト。</param>
    /// <returns>バックアップ結果を表す BackupResult オブジェクト。</returns>
    Task<BackupResult> RunBackupAsync(BackupRequest request);
}