using KBackup.Core.Interfaces;

namespace KBackup.Core.Models;

/// <summary>
/// バックアップ処理の結果を表すクラス。
/// </summary>
public sealed class BackupResult : IOperationResult {
    /// <summary>バックアップ全体が成功したかどうか。致命的エラーがなければ true。</summary>
    public bool Success { get; set; } = true;
    /// <summary>バックアップを保存したディレクトリ情報。</summary>
    public DirectoryInfo? BackedUpDirectory { get; set; }
    /// <summary>JSON レコードの保存に成功したか。</summary>
    public bool JsonSaved { get; set; }
    /// <summary>フィールドスキーマの保存に成功したか。</summary>
    public bool SchemaSaved { get; set; }
    /// <summary>保存したレコード数。</summary>
    public int RecordCount { get; set; }
    /// <summary>添付ファイルのダウンロード成功数。</summary>
    public int FileDownloadedCount { get; set; }
    /// <summary>添付ファイルのダウンロード失敗数。</summary>
    public int FileFailedCount { get; set; }
    /// <summary>スキップされた処理のリスト。</summary>
    public List<string> Skipped { get; set; } = [];
    /// <summary>警告メッセージのリスト。</summary>
    public List<string> Warnings { get; set; } = [];
    /// <summary>致命的エラーメッセージのリスト。</summary>
    public List<string> Errors { get; set; } = [];
    /// <summary>警告があるかどうか。</summary>
    public bool HasWarnings => this.Warnings.Count > 0 || this.FileFailedCount > 0;
    /// <summary>成功だが警告ありの状態かどうか。</summary>
    public bool IsPartialSuccess => this.Success && this.HasWarnings;
    /// <summary>バックアップが複数のファイルに分割された場合の各ファイルのパスリスト。</summary>
    public IList<string> PartFiles { get; set; } = [];
    /// <summary>バックアップが分割されたファイル数。</summary>
    public int Parts => this.PartFiles.Count;
    /// <summary>バックアップデータの分割サイズ（1ファイルあたりのレコード数）。</summary>
    public int SplitSize { get; set; }

    IReadOnlyList<string> IOperationResult.Warnings => this.Warnings;
    IReadOnlyList<string> IOperationResult.Errors => this.Errors;
}
