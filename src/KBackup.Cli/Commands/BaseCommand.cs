using System.CommandLine;
using KBackup.Cli.Config;
using Microsoft.Extensions.Logging;

namespace KBackup.Cli.Commands;

/// <summary>
/// すべてのコマンドの基底クラス。共通のオプションやロジックを定義する。
/// </summary>
public abstract class BaseCommand : Command {
    protected Option<string?> ConfigOption { get; }
    protected ILogger<BaseCommand> _logger;

    /// <summary>
    /// コンストラクタ。コマンドの名前、説明、および共通オプションを定義する。
    /// </summary>
    /// <param name="name">コマンドの名前</param>
    /// <param name="description">コマンドの説明</param>
    /// <param name="logger">ロガーオブジェクト</param>
    protected BaseCommand(string name, string description, ILogger<BaseCommand> logger) : base(name, description) {
        this._logger = logger;

        this.ConfigOption = new Option<string?>("--config") {
            Description = "Path to kbackup.config.json"
        };

        this.Add(this.ConfigOption);
    }

    /// <summary>
    /// コマンドの実行前に、共通の設定を読み込むためのメソッド。コマンド固有のオプションと組み合わせて、最終的な設定を構築する。
    /// </summary>
    /// <param name="parseResult">コマンドライン引数の解析結果</param>
    /// <returns>読み込まれた設定オブジェクト、または <c>null</c></returns>
    protected KBackupConfig? LoadConfig(ParseResult parseResult) {
        var configPath = parseResult.GetValue(this.ConfigOption);
        return ConfigLoader.Load(configPath);
    }

}
