namespace KBackup.Core.Models;

/// <summary>
/// BaseKintoneRequest クラスは、Kintone API に対するリクエストの基本的なプロパティを定義する抽象クラスであり、SubDomain、AppId、および ApiToken のプロパティが含まれています。これらのプロパティは、Kintone API にアクセスするために必要な情報を提供し、これらのプロパティを継承するクラスで使用されます。
/// </summary>
public abstract class BaseKintoneRequest {
    /// <summary>
    /// SubDomain プロパティは、Kintone のサブドメインを表す文字列型のプロパティであり、必須の初期化プロパティとして定義されています。このプロパティは、Kintone API にアクセスする際に使用されるサブドメインを指定するために使用されます。
    /// </summary>
    public required string SubDomain { get; init; }

    /// <summary>
    /// AppId プロパティは、Kintone のアプリケーション ID を表す整数型のプロパティであり、必須の初期化プロパティとして定義されています。このプロパティは、Kintone API にアクセスする際に使用されるアプリケーション ID を指定するために使用されます。
    /// </summary>
    public required int AppId { get; init; }

    /// <summary>
    /// ApiToken プロパティは、Kintone API にアクセスするための API トークンを表す文字列型のプロパティであり、必須の初期化プロパティとして定義されています。このプロパティは、Kintone API にアクセスする際に使用される API トークンを指定するために使用されます。
    /// </summary>
    public required string ApiToken { get; init; }
}