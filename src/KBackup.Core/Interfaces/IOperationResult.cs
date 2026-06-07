namespace KBackup.Core.Interfaces;

/// <summary>
/// バックアップ・リストア操作の結果を表すインターフェース。
/// </summary>
public interface IOperationResult {
    /// <summary>処理全体が成功したかどうか。</summary>
    bool Success { get; }
    /// <summary>成功だが警告ありの状態かどうか。</summary>
    bool IsPartialSuccess { get; }
    /// <summary>警告メッセージのリスト。</summary>
    IReadOnlyList<string> Warnings { get; }
    /// <summary>エラーメッセージのリスト。</summary>
    IReadOnlyList<string> Errors { get; }
}
