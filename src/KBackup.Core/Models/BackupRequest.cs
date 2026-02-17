using KBackup.Core.Enums;

namespace KBackup.Core.Models;

/// <summary>
/// BackupRequest クラスは、バックアップリクエストを表すモデルクラスであり、BaseKintoneRequest クラスを継承しています。このクラスには、バックアップの出力ディレクトリや、バックアップオプションに対応するプロパティが定義されています。これらのプロパティは、バックアップの動作を制御するために使用されます。
/// </summary>
public sealed class BackupRequest : BaseKintoneRequest {
    /// <summary>
    /// OutputDirectory プロパティは、バックアップの出力先ディレクトリを表す DirectoryInfo 型のプロパティであり、必須の初期化プロパティとして定義されています。このプロパティは、バックアップされたデータが保存されるディレクトリを指定するために使用されます。
    /// </summary>
    public required DirectoryInfo OutputDirectory { get; init; }

    // BackupOptions に対応する項目
    /// <summary>
    /// DownloadFiles プロパティは、バックアップオプションの一つであり、ファイルをダウンロードするかどうかを示すブール値のプロパティです。デフォルト値は true に設定されており、ファイルのダウンロードが有効になっています。このプロパティは、バックアッププロセス中にファイルをダウンロードするかどうかを制御するために使用されます。
    /// </summary>
    public bool DownloadFiles { get; init; } = true;

    /// <summary>
    /// IncludeFieldSchema プロパティは、バックアップオプションの一つであり、フィールドスキーマを含めるかどうかを示すブール値のプロパティです。デフォルト値は true に設定されており、フィールドスキーマがバックアップに含まれるようになっています。このプロパティは、バックアッププロセス中にフィールドスキーマを含めるかどうかを制御するために使用されます。
    /// </summary>
    public bool IncludeFieldSchema { get; init; } = true;

    /// <summary>
    /// Overwrite プロパティは、バックアップオプションの一つであり、既存のバックアップファイルを上書きするかどうかを示すブール値のプロパティです。デフォルト値は false に設定されており、既存のバックアップファイルが存在する場合は上書きされません。このプロパティは、バックアッププロセス中に既存のバックアップファイルを上書きするかどうかを制御するために使用されます。
    /// </summary>
    public bool Overwrite { get; init; } = false;

    /// <summary>
    /// Pretty プロパティは、バックアップオプションの一つであり、出力される JSON データを整形するかどうかを示すブール値のプロパティです。デフォルト値は false に設定されており、JSON データは整形されません。このプロパティは、バックアッププロセス中に出力される JSON データを整形するかどうかを制御するために使用されます。
    /// </summary>
    public bool Pretty { get; init; } = false;

    /// <summary>
    /// EscapeUnicode プロパティは、バックアップオプションの一つであり、Unicode 文字をエスケープするかどうかを示すブール値のプロパティです。デフォルト値は true に設定されており、Unicode 文字がエスケープされるようになっています。このプロパティは、バックアッププロセス中に Unicode 文字をエスケープするかどうかを制御するために使用されます。
    /// </summary>
    public bool EscapeUnicode { get; init; } = true;

    /// <summary>
    /// Query プロパティは、バックアップオプションの一つであり、バックアップ対象のレコードをフィルタリングするためのクエリ文字列を表す文字列型のプロパティです。デフォルト値は null に設定されており、クエリが指定されていない場合はすべてのレコードがバックアップされます。このプロパティは、バックアッププロセス中に特定の条件に一致するレコードのみをバックアップするために使用されます。
    /// </summary>
    public string? Query { get; init; } = null;

    /// <summary>
    /// SplitSize プロパティは、バックアップオプションの一つであり、バックアップファイルを分割する際の分割サイズを表す整数型のプロパティです。デフォルト値は 1000 に設定されており、バックアップファイルが 1000 レコードごとに分割されるようになっています。このプロパティは、バックアッププロセス中にバックアップファイルを分割する際の分割サイズを制御するために使用されます。
    /// </summary>
    public int SplitSize { get; init; } = 1000;

    /// <summary>
    /// ColorMode プロパティは、バックアップオプションの一つであり、ログ出力のカラーモードを表す ColorMode 列挙型のプロパティです。デフォルト値は ColorMode.Auto に設定されており、環境に応じて自動的にカラーモードが選択されます。このプロパティは、バックアッププロセス中のログ出力のカラーモードを制御するために使用されます。
    /// </summary>
    public ColorMode ColorMode { get; init; } = ColorMode.Auto;

    /// <summary>
    /// JsonOutput プロパティは、バックアップオプションの一つであり、バックアップ結果を JSON 形式で出力するかどうかを示すブール値のプロパティです。デフォルト値は false に設定されており、バックアップ結果は JSON 形式で出力されません。このプロパティは、バックアッププロセス中にバックアップ結果を JSON 形式で出力するかどうかを制御するために使用されます。
    /// </summary>
    public bool JsonOutput { get; init; } = false;
}
