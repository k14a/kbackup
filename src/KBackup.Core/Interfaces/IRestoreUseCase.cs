using KBackup.Core.Models;

namespace KBackup.Core.Interfaces;

/// <summary>
/// IRestoreUseCase インターフェースは、リストア処理のユースケースを定義するインターフェースです。RunRestoreAsync メソッドを通じて、リストアリクエストを受け取り、リストア結果を返す非同期メソッドを提供します。
/// </summary>
public interface IRestoreUseCase {
    /// <summary>
    /// RunRestoreAsync メソッドは、指定された RestoreRequest を使用してリストアを実行し、RestoreResult を返します。
    /// </summary>
    /// <param name="request">リストアリクエストを表す RestoreRequest オブジェクト。</param>
    /// <returns>リストア結果を表す RestoreResult オブジェクト。</returns>
    Task<RestoreResult> RunRestoreAsync(RestoreRequest request);
}
