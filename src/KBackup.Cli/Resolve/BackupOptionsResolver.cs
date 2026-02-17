using System.CommandLine;
using KBackup.Cli.Commands;
using KBackup.Cli.Config;
using KBackup.Cli.Factories;
using KBackup.Cli.Options;
using KintoneNetLibrary.Domain.Common;

namespace KBackup.Cli.Resolve;

/// <summary>
/// BackupOptionsResolver クラスは、コマンドライン引数と設定ファイルから BackupOptions オブジェクトを生成するためのクラスです。
/// </summary>
public static class BackupOptionsResolver {
    /// <summary>
    /// Resolve メソッドは、コマンドライン引数と設定ファイルから BackupOptions オブジェクトを生成します。コマンドライン引数が優先され、設定ファイルの値はコマンドライン引数が提供されない場合に使用されます。
    /// </summary>
    /// <param name="result">コマンドライン引数の解析結果を表す ParseResult オブジェクト。</param>
    /// <param name="config">設定ファイルから読み込まれた KBackupConfig オブジェクト。</param>
    /// <returns>生成された BackupOptions オブジェクト。</returns>
    public static BackupOptions Resolve(ParseResult result, KBackupConfig? config) {
        return new BackupOptions {
            // BaseKintoneRequest
            SubDomain = BaseCommand.Resolve(result, OptionFactory.SubDomain, config?.SubDomain),
            AppId = BaseCommand.Resolve(result, OptionFactory.AppId, config?.AppId ?? 0),
            ApiToken = BaseCommand.Resolve(result, OptionFactory.ApiToken, config?.ApiToken),

            // BackupRequest
            OutputDir = BaseCommand.Resolve(result, OptionFactory.OutputDir, config?.BackupDirectory),
            DownloadFiles = BaseCommand.Resolve(result, OptionFactory.DownloadFiles, config?.DownloadFiles ?? false),
            IncludeFieldSchema = BaseCommand.Resolve(result, OptionFactory.IncludeFieldSchema, config?.IncludeFieldSchema ?? false),
            Overwrite = BaseCommand.Resolve(result, OptionFactory.Overwrite, config?.Overwrite ?? false),
            Pretty = BaseCommand.Resolve(result, OptionFactory.Pretty, config?.Pretty ?? false),
            EscapeUnicode = BaseCommand.Resolve(result, OptionFactory.EscapeUnicode, config?.EscapeUnicode ?? false),
            Query = BaseCommand.Resolve(result, OptionFactory.Query, config?.Query ?? string.Empty),
            SplitSize = BaseCommand.Resolve(result, OptionFactory.Split, config?.SplitSize ?? 1000),

            // Output settings
            ColorMode = BaseCommand.Resolve(result, OptionFactory.OutputColorMode, config?.ColorMode ?? Core.Enums.ColorMode.Auto),
            JsonOutput = BaseCommand.Resolve(result, OptionFactory.JsonOutput, config?.JsonOutput ?? false)
        };
    }
}