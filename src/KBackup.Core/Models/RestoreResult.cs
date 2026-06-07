using KBackup.Core.Interfaces;

namespace KBackup.Core.Models;

/// <summary>
/// リストア処理の結果を表すクラス。
/// </summary>
public sealed class RestoreResult : IOperationResult {
    /// <summary>リストア全体が成功したかどうか。</summary>
    public bool Success { get; set; } = true;
    /// <summary>追加されたレコード数。</summary>
    public int AddedRecords { get; set; }
    /// <summary>更新されたレコード数。</summary>
    public int UpdatedRecords { get; set; }
    /// <summary>削除されたレコード数。</summary>
    public int DeletedRecords { get; set; }
    /// <summary>添付ファイルのアップロード成功数。</summary>
    public int UploadedFiles { get; set; }
    /// <summary>添付ファイルのアップロード失敗数。</summary>
    public int FailedFiles { get; set; }
    /// <summary>警告メッセージのリスト。</summary>
    public List<string> Warnings { get; set; } = [];
    /// <summary>致命的エラーメッセージのリスト。</summary>
    public List<string> Errors { get; set; } = [];
    /// <summary>警告があるかどうか。</summary>
    public bool HasWarnings => this.Warnings.Count > 0 || this.FailedFiles > 0;
    /// <summary>致命的エラーがあるかどうか。</summary>
    public bool HasErrors => this.Errors.Count > 0 || !this.Success;
    /// <summary>成功だが警告ありの状態かどうか。</summary>
    public bool IsPartialSuccess => this.Success && this.HasWarnings;

    IReadOnlyList<string> IOperationResult.Warnings => this.Warnings;
    IReadOnlyList<string> IOperationResult.Errors => this.Errors;
}
