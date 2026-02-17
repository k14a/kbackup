using KBackup.Core.Enums;
using KintoneNetLibrary.Backup.Domain.Enums;

namespace KBackup.Cli.Options;

/// <summary>
/// RestoreOptions クラスは、リストアコマンドのオプションを表すクラスです。
/// </summary>
public sealed class RestoreOptions {
    // BaseKintoneRequest に対応
    /// <summary>
    /// SubDomain、AppId、ApiToken は、Kintone API にアクセスするための基本的な認証情報を表します。AppId は、リストア対象のアプリケーションIDを指定します。ApiToken は、Kintone API へのアクセスに使用されるAPIトークンを指定します。
    /// </summary>
    public required string SubDomain { get; init; }

    /// <summary>
    /// AppId は、リストア対象のアプリケーションIDを指定します。ApiToken は、Kintone API へのアクセスに使用されるAPIトークンを指定します。
    /// </summary>
    public required int AppId { get; init; }

    /// <summary>
    /// ApiToken は、Kintone API へのアクセスに使用されるAPIトークンを指定します。
    /// </summary>
    public required string ApiToken { get; init; }

    // RestoreRequest に対応
    /// <summary>
    /// BackupDir は、リストア元のバックアップディレクトリを指定します。Mode は、リストアのモードを指定します。RestoreFiles は、添付ファイルやスペースのファイルをリストアするかどうかを指定します。Force は、既存のデータを上書きするかどうかを指定します。
    /// </summary>
    public required string BackupDir { get; init; }

    /// <summary>
    /// Mode は、リストアのモードを指定します。RestoreFiles は、添付ファイルやスペースのファイルをリストアするかどうかを指定します。Force は、既存のデータを上書きするかどうかを指定します。
    /// </summary>
    public RestoreMode Mode { get; init; }

    /// <summary>
    /// RestoreFiles は、添付ファイルやスペースのファイルをリストアするかどうかを指定します。Force は、既存のデータを上書きするかどうかを指定します。
    /// </summary>
    public bool RestoreFiles { get; init; }

    /// <summary>
    /// Force は、既存のデータを上書きするかどうかを指定します。
    /// </summary>
    public bool Force { get; init; }

    /// <summary>
    /// DryRun は、リストアの実行前に、どのような操作が行われるかをシミュレーションするためのオプションです。ValidateOnly は、リストアの実行前に、リストアの設定が正しいかどうかを検証するためのオプションです。BatchSize は、リストア処理をバッチ単位で実行する際のバッチサイズを指定します。
    /// </summary>
    public bool DryRun { get; init; }

    /// <summary>
    /// ValidateOnly は、リストアの実行前に、リストアの設定が正しいかどうかを検証するためのオプションです。
    /// </summary>
    public bool ValidateOnly { get; init; }

    /// <summary>
    /// BatchSize は、リストア処理をバッチ単位で実行する際のバッチサイズを指定します。
    /// </summary>
    public int BatchSize { get; init; }

    // 出力設定
    /// <summary>
    /// ColorMode は、コマンドライン出力の色のモードを指定します。JsonOutput は、出力をJSON形式で表示するかどうかを指定します。
    /// </summary>
    public ColorMode ColorMode { get; init; }

    /// <summary>
    /// JsonOutput は、出力をJSON形式で表示するかどうかを指定します。
    /// </summary>
    public bool JsonOutput { get; init; }
}