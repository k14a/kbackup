namespace KBackup.Core;

/// <summary>
/// KintoneBackupData クラスは、Kintone のバックアップデータを表すモデルクラスであり、アプリ名、スキーマ、レコード、およびファイルの情報をプロパティとして持ちます。これらのプロパティは、Kintone のバックアップデータを構造化された形式で表現するために使用されます。
/// </summary>
public class KintoneBackupData {
    /// <summary>
    /// AppName プロパティは、Kintone のアプリケーション名を表す文字列型のプロパティであり、バックアップデータのアプリケーション名を指定するために使用されます。
    /// </summary>
    public string AppName { get; set; } = "";

    /// <summary>
    /// Schema プロパティは、Kintone のバックアップデータのスキーマ情報を表すオブジェクト型のプロパティであり、バックアップデータのスキーマ情報を指定するために使用されます。
    /// </summary>
    public object Schema { get; set; } = default!;

    /// <summary>
    /// Records プロパティは、Kintone のバックアップデータのレコード情報を表すオブジェクト型のプロパティであり、バックアップデータのレコード情報を指定するために使用されます。
    /// </summary>
    public object Records { get; set; } = default!;

    /// <summary>
    /// Files プロパティは、Kintone のバックアップデータのファイル情報を表す List<KintoneFileData> 型のプロパティであり、バックアップデータのファイル情報を指定するために使用されます。
    /// </summary>
    public List<KintoneFileData> Files { get; set; } = [];
}

/// <summary>
/// KintoneFileData クラスは、Kintone のファイルデータを表すモデルクラスであり、ファイルキー、ファイル名、およびファイルの内容をプロパティとして持ちます。これらのプロパティは、Kintone のバックアップデータに含まれるファイル情報を構造化された形式で表現するために使用されます。
/// </summary>
public class KintoneFileData {
    /// <summary>
    /// FileKey プロパティは、Kintone のファイルキーを表す文字列型のプロパティであり、バックアップデータのファイルキーを指定するために使用されます。
    /// </summary>
    public string FileKey { get; set; } = "";

    /// <summary>
    /// FileName プロパティは、Kintone のファイル名を表す文字列型のプロパティであり、バックアップデータのファイル名を指定するために使用されます。
    /// </summary>
    public string FileName { get; set; } = "";

    /// <summary>
    /// Content プロパティは、Kintone のファイルの内容を表すバイト配列型のプロパティであり、バックアップデータのファイルの内容を指定するために使用されます。
    /// </summary>
    public byte[] Content { get; set; } = Array.Empty<byte>();
}
