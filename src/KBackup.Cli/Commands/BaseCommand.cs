using System.CommandLine;
using KBackup.Cli.Config;
using KBackup.Core.Enums;
using Microsoft.Extensions.Logging;

namespace KBackup.Cli.Commands;

/// <summary>
/// すべてのコマンドの基底クラス。共通のオプションやロジックを定義する。
/// </summary>
public abstract class BaseCommand : Command {
    protected Option<string?> ConfigOption { get; }
    protected Option<ColorMode> ColorOption { get; }
    protected Option<bool> JsonOption { get; }
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

        this.ColorOption = new Option<ColorMode>("--color") {
            DefaultValueFactory = _ => ColorMode.Auto,
            Description = "Color output mode: auto, always, never"
        };

        this.JsonOption = new Option<bool>("--json") {
            Description = "Output result as JSON"
        };

        this.Add(this.ConfigOption);
        this.Add(this.ColorOption);
        this.Add(this.JsonOption);
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

    /// <summary>
    /// コマンドのオプションを解決するためのユーティリティメソッド。コマンドライン引数、設定ファイル、およびオプションのデフォルト値の優先順位に従って、最終的な値を決定する。
    /// </summary>
    /// <typeparam name="T">オプションの型</typeparam>
    /// <param name="result">コマンドライン引数の解析結果</param>
    /// <param name="option">対象のオプション</param>
    /// <param name="configValue">設定ファイルから取得した値</param>
    /// <returns>最終的に決定されたオプションの値</returns>
    public static T Resolve<T>(ParseResult result, Option<T> option, T? configValue) {
        var value = result.GetValue(option);
        if (value is not null) { return value; }
        if (configValue is not null) { return configValue; }
        return (T)option.GetDefaultValue()!;
    }
}
