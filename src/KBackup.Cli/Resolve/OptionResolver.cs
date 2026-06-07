using System.CommandLine;

namespace KBackup.Cli.Resolve;

/// <summary>
/// コマンドラインオプションの値を解決するユーティリティクラス。
/// コマンドライン引数・設定ファイル・オプションデフォルト値の優先順位に従って値を決定する。
/// </summary>
public static class OptionResolver {
    /// <summary>
    /// コマンドライン引数、設定ファイル、オプションデフォルト値の順で優先して値を返す。
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
