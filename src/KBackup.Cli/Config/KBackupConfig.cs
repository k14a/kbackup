using KBackup.Core.Enums;
using KintoneNetLibrary.Backup.Domain.Enums;
using KintoneNetLibrary.Domain.Common;

namespace KBackup.Cli.Config;

/// <summary>
/// KBackupの設定を表すクラス。JSON形式の設定ファイルから読み込まれることを想定している。
/// </summary>
public sealed class KBackupConfig {
    /// <summary>
    /// kintoneのサブドメイン。例: "example"（URLが "https://example.kintone.com" の場合）。
    /// </summary>
    public string SubDomain { get; set; } = string.Empty;
    /// <summary>
    /// kintoneのAPIトークン。
    /// </summary>
    public string ApiToken { get; set; } = string.Empty;
    /// <summary>
    /// kintoneのアプリID。
    /// </summary>
    public int AppId { get; set; } = 0;
    #region <<for BackupRequest>>
    /// <summary>
    /// バックアップファイルの出力先ディレクトリ。例: "C:\\backups"。デフォルトはカレントディレクトリ。
    /// </summary>
    public string OutputDirectory { get; set; } = string.Empty;
    /// <summary>
    /// ファイルもバックアップするかどうか。デフォルトは <c>true</c>。
    /// </summary>
    public bool DownloadFiles { get; set; } = true;
    /// <summary>
    /// フィールドのスキーマ情報もバックアップするかどうか。デフォルトは <c>true</c>。
    /// </summary>
    public bool IncludeFieldSchema { get; set; } = true;
    /// <summary>
    /// 既存のバックアップファイルを上書きするかどうか。デフォルトは <c>false</c>。
    /// </summary>
    public bool Overwrite { get; set; } = false;
    /// <summary>
    /// 強制的に操作を実行するかどうか。デフォルトは <c>false</c>。
    /// </summary>
    public bool Force { get; set; } = false;
    /// <summary>
    /// JSON出力を整形するかどうか。デフォルトは <c>false</c>。
    /// </summary>
    public bool Pretty { get; set; } = false;
    /// <summary>
    /// Unicode文字をエスケープするかどうか。デフォルトは <c>true</c>。
    /// </summary>
    public bool EscapeUnicode { get; set; } = true;
    /// <summary>
    /// クエリ文字列。デフォルトは空文字列。
    /// </summary>
    public string Query { get; set; } = string.Empty;
    /// <summary>
    /// 分割サイズ。デフォルトは1000。
    /// </summary>
    public int SplitSize { get; set; } = 1000;
    #endregion
    #region <<for RestoreRequest>>
    /// <summary>
    /// バックアップディレクトリ。デフォルトは <c>null</c>。
    /// </summary>
    public string? BackupDirectory { get; set; }
    /// <summary>
    /// リストアモード。デフォルトは <c>Upsert</c>。
    /// </summary>
    public RestoreMode Mode { get; set; } = RestoreMode.Upsert;
    /// <summary>
    /// ファイルもリストアするかどうか。デフォルトは <c>true</c>。
    /// </summary>
    public bool RestoreFiles { get; set; } = true;
    /// <summary>
    /// ドライランモードかどうか。デフォルトは <c>false</c>。
    /// </summary>
    public bool DryRun { get; set; } = false;
    /// <summary>
    /// バリデーションのみを実行するかどうか。デフォルトは <c>false</c>。
    /// </summary>
    public bool ValidateOnly { get; set; } = false;
    /// <summary>
    /// バッチサイズ。デフォルトは KintoneConstants.KintoneLimit。
    /// </summary>
    public int BatchSize { get; set; } = KintoneConstants.KintoneLimit;
    #endregion
    /// <summary>
    /// カラーモード。デフォルトは <c>Auto</c>。
    /// </summary>
    public ColorMode ColorMode { get; set; } = ColorMode.Auto;
    /// <summary>
    /// JSON出力を行うかどうか。デフォルトは <c>false</c>。
    /// </summary>
    public bool JsonOutput { get; set; } = false;
}