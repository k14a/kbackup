using KBackup.Core.Models;

namespace KBackup.Core.Interfaces;

/// <summary>
/// Kintone へのリストア操作を定義するインターフェース。
/// </summary>
public interface IRestoreKintoneRepository {
    /// <summary>
    /// 指定された RestoreRequest を使用してリストアを実行し、RestoreResult を返します。
    /// </summary>
    /// <param name="request">リストアリクエストを表す RestoreRequest オブジェクト。</param>
    /// <returns>リストア結果を表す RestoreResult オブジェクト。</returns>
    Task<RestoreResult> RestoreAsync(RestoreRequest request);
}
