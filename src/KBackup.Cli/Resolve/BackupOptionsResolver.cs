using System.CommandLine;
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
            SubDomain = OptionResolver.Resolve(result, OptionFactory.SubDomain, config?.SubDomain),
            AppId = OptionResolver.Resolve(result, OptionFactory.AppId, config?.AppId ?? 0),
            ApiToken = OptionResolver.Resolve(result, OptionFactory.ApiToken, config?.ApiToken),

            // BackupRequest
            OutputDirectory = OptionResolver.Resolve(result, OptionFactory.OutputDir, config?.OutputDirectory),
            DownloadFiles = OptionResolver.Resolve(result, OptionFactory.DownloadFiles, config?.DownloadFiles ?? false),
            IncludeFieldSchema = OptionResolver.Resolve(result, OptionFactory.IncludeFieldSchema, config?.IncludeFieldSchema ?? false),
            Overwrite = OptionResolver.Resolve(result, OptionFactory.Overwrite, config?.Overwrite ?? false),
            Pretty = OptionResolver.Resolve(result, OptionFactory.Pretty, config?.Pretty ?? false),
            EscapeUnicode = OptionResolver.Resolve(result, OptionFactory.EscapeUnicode, config?.EscapeUnicode ?? false),
            Query = OptionResolver.Resolve(result, OptionFactory.Query, config?.Query ?? string.Empty),
            SplitSize = OptionResolver.Resolve(result, OptionFactory.SplitSize, config?.SplitSize ?? 1000),
            FieldCodes = null,
            BatchSize = null,

            // Output settings
            ColorMode = OptionResolver.Resolve(result, OptionFactory.ColorMode, config?.ColorMode ?? Core.Enums.ColorMode.Auto),
            JsonOutput = OptionResolver.Resolve(result, OptionFactory.JsonOutput, config?.JsonOutput ?? false)
        };
    }
}