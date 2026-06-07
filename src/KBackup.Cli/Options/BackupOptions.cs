using KBackup.Core.Enums;

namespace KBackup.Cli.Options;

/// <summary>
/// BackupOptions クラスは、バックアップコマンドのオプションを表すクラスです。
/// これらのオプションは、コマンドライン引数から設定され、BackupRequest を構築する際に使用されます。
/// </summary>
public sealed class BackupOptions {
    /// <summary>
    /// SubDomain、AppId、ApiToken は、Kintone API にアクセスするための基本的な認証情報を表します。
    /// </summary>
    public required string SubDomain { get; init; }

    /// <summary>
    /// AppId は、バックアップ対象のアプリケーションIDを指定します。ApiToken は、Kintone API へのアクセスに使用されるAPIトークンを指定します。
    /// </summary>
    public required int AppId { get; init; }

    /// <summary>
    /// ApiToken は、Kintone API へのアクセスに使用されるAPIトークンを指定します。
    /// </summary>
    public required string ApiToken { get; init; }

    /// <summary>
    /// OutputDir は、バックアップファイルの出力先ディレクトリを指定します。DownloadFiles は、添付ファイルやスペースのファイルをダウンロードするかどうかを指定します。
    /// IncludeFieldSchema は、フィールドのスキーマ情報をバックアップに含めるかどうかを指定します。Overwrite は、既存のバックアップファイルを上書きするかどうかを指定します。
    /// Pretty は、出力されるJSONを整形するかどうかを指定します。EscapeUnicode は、JSON出力でUnicode文字をエスケープするかどうかを指定します。Query は、バックアップ対象のレコードを絞り込むためのクエリを指定します。SplitSize は、バックアップファイルを分割するサイズ（MB単位）を指定します。
    /// </summary>
    public required string OutputDirectory { get; init; }

    /// <summary>
    /// Mode は、バックアップのモードを指定します。ColorMode は、コマンドライン出力のカラー表示のモードを指定します。JsonOutput は、コマンドライン出力をJSON形式にするかどうかを指定します。
    /// </summary>
    public bool DownloadFiles { get; init; }

    /// <summary>
    /// IncludeFieldSchema は、フィールドのスキーマ情報をバックアップに含めるかどうかを指定します。Overwrite は、既存のバックアップファイルを上書きするかどうかを指定します。Pretty は、出力されるJSONを整形するかどうかを指定します。EscapeUnicode は、JSON出力でUnicode文字をエスケープするかどうかを指定します。Query は、バックアップ対象のレコードを絞り込むためのクエリを指定します。SplitSize は、バックアップファイルを分割するサイズ（MB単位）を指定します。
    /// </summary>
    public bool IncludeFieldSchema { get; init; }

    /// <summary>
    /// Overwrite は、既存のバックアップファイルを上書きするかどうかを指定します。Pretty は、出力されるJSONを整形するかどうかを指定します。EscapeUnicode は、JSON出力でUnicode文字をエスケープするかどうかを指定します。Query は、バックアップ対象のレコードを絞り込むためのクエリを指定します。SplitSize は、バックアップファイルを分割するサイズ（MB単位）を指定します。
    /// </summary>
    public bool Overwrite { get; init; }

    /// <summary>
    /// Pretty は、出力されるJSONを整形するかどうかを指定します。EscapeUnicode は、JSON出力でUnicode文字をエスケープするかどうかを指定します。Query は、バックアップ対象のレコードを絞り込むためのクエリを指定します。SplitSize は、バックアップファイルを分割するサイズ（MB単位）を指定します。
    /// </summary>
    public bool Pretty { get; init; }

    /// <summary>
    /// EscapeUnicode は、JSON出力でUnicode文字をエスケープするかどうかを指定します。Query は、バックアップ対象のレコードを絞り込むためのクエリを指定します。SplitSize は、バックアップファイルを分割するサイズ（MB単位）を指定します。
    /// </summary>
    public bool EscapeUnicode { get; init; }

    /// <summary>
    /// Query は、バックアップ対象のレコードを絞り込むためのクエリを指定します。SplitSize は、バックアップファイルを分割するサイズ（MB単位）を指定します。
    /// </summary>
    public string? Query { get; init; }

    /// <summary>
    /// SplitSize は、バックアップファイルを分割するサイズ（MB単位）を指定します。
    /// </summary>
    public int SplitSize { get; init; }

    /// <summary>
    /// 取得するフィールドコードのリスト。null の場合は全フィールドを取得します。
    /// </summary>
    public IList<string>? FieldCodes { get; init; }

    /// <summary>
    /// カーソルページサイズ。null の場合は KintoneNetLibrary のデフォルト値を使用します。
    /// </summary>
    public int? BatchSize { get; init; }

    // 出力設定
    /// <summary>
    /// Mode は、バックアップのモードを指定します。ColorMode は、コマンドライン出力のカラー表示のモードを指定します。JsonOutput は、コマンドライン出力をJSON形式にするかどうかを指定します。
    /// </summary>
    public ColorMode ColorMode { get; init; }

    /// <summary>
    /// JsonOutput は、コマンドライン出力をJSON形式にするかどうかを指定します。
    /// </summary>
    public bool JsonOutput { get; init; }
}