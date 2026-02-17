using KBackup.Core.Enums;
using KintoneNetLibrary.Backup.Domain.Enums;
using KintoneNetLibrary.Domain.Common;

namespace KBackup.Core.Models;

/// <summary>
/// RestoreRequest クラスは、リストアリクエストを表すモデルクラスであり、BaseKintoneRequest クラスを継承しています。このクラスには、バックアップディレクトリや、リストアオプションに対応するプロパティが定義されています。これらのプロパティは、リストアの動作を制御するために使用されます。
/// </summary>
public sealed class RestoreRequest : BaseKintoneRequest {
    /// <summary>
    /// BackupDirectory プロパティは、リストアの元となるバックアップデータが保存されているディレクトリを表す DirectoryInfo 型のプロパティであり、必須の初期化プロパティとして定義されています。このプロパティは、リストアプロセス中に使用されるバックアップデータの場所を指定するために使用されます。
    /// </summary>
    public required DirectoryInfo BackupDirectory { get; init; }

    // RestoreOptions に対応する項目
    /// <summary>
    /// Mode プロパティは、リストアオプションの一つであり、リストアのモードを表す RestoreMode 列挙型のプロパティです。このプロパティは、リストアプロセス中に使用されるリストアのモードを指定するために使用されます。
    /// </summary>
    public RestoreMode Mode { get; init; }

    /// <summary>
    /// RestoreFiles プロパティは、リストアオプションの一つであり、ファイルをリストアするかどうかを示すブール値のプロパティです。このプロパティは、リストアプロセス中にファイルをリストアするかどうかを制御するために使用されます。
    /// </summary>
    public bool RestoreFiles { get; init; }

    /// <summary>
    /// Force プロパティは、リストアオプションの一つであり、リストアを強制的に実行するかどうかを示すブール値のプロパティです。このプロパティは、リストアプロセス中にリストアを強制的に実行するかどうかを制御するために使用されます。
    /// </summary>
    public bool Force { get; init; }

    /// <summary>
    /// DryRun プロパティは、リストアオプションの一つであり、リストアを実際には実行せずに、リストアプロセスをシミュレートするかどうかを示すブール値のプロパティです。このプロパティは、リストアプロセスをシミュレートするかどうかを制御するために使用されます。
    /// </summary>
    public bool DryRun { get; init; }

    /// <summary>
    /// ValidateOnly プロパティは、リストアオプションの一つであり、リストアを実際には実行せずに、リストアプロセスの検証のみを行うかどうかを示すブール値のプロパティです。このプロパティは、リストアプロセスの検証のみを行うかどうかを制御するために使用されます。
    /// </summary>
    public bool ValidateOnly { get; init; }

    /// <summary>
    /// BatchSize プロパティは、リストアオプションの一つであり、リストアプロセス中にレコードをバッチ処理する際のバッチサイズを表す整数型のプロパティです。このプロパティは、リストアプロセス中にレコードをバッチ処理する際のバッチサイズを制御するために使用されます。
    /// </summary>
    public int BatchSize { get; init; }

    /// <summary>
    /// ColorMode プロパティは、リストアオプションの一つであり、ログ出力のカラーモードを表す ColorMode 列挙型のプロパティです。このプロパティは、リストアプロセス中のログ出力のカラーモードを制御するために使用されます。
    /// </summary>
    public ColorMode ColorMode { get; init; }

    /// <summary>
    /// JsonOutput プロパティは、リストアオプションの一つであり、リストア結果を JSON 形式で出力するかどうかを示すブール値のプロパティです。このプロパティは、リストアプロセス中にリストア結果を JSON 形式で出力するかどうかを制御するために使用されます。
    /// </summary>
    public bool JsonOutput { get; init; }
}
