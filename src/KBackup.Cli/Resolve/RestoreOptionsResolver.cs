using System.CommandLine;
using KBackup.Cli.Config;
using KBackup.Cli.Factories;
using KBackup.Cli.Options;
using KintoneNetLibrary.Domain.Common;

namespace KBackup.Cli.Resolve;

/// <summary>
/// RestoreOptionsResolver クラスは、コマンドライン引数と設定ファイルから RestoreOptions オブジェクトを生成するためのクラスです。
/// </summary>
public static class RestoreOptionsResolver {
    /// <summary>
    /// Resolve メソッドは、コマンドライン引数と設定ファイルから RestoreOptions オブジェクトを生成します。コマンドライン引数が優先され、設定ファイルの値はコマンドライン引数が提供されない場合に使用されます。
    /// </summary>
    /// <param name="result">コマンドライン引数の解析結果を表す ParseResult オブジェクト。</param>
    /// <param name="config">設定ファイルから読み込まれた KBackupConfig オブジェクト。</param>
    /// <returns>生成された RestoreOptions オブジェクト。</returns>
    public static RestoreOptions Resolve(ParseResult result, KBackupConfig? config) {
        return new RestoreOptions {
            // BaseKintoneRequest
            SubDomain = OptionResolver.Resolve(result, OptionFactory.SubDomain, config?.SubDomain),
            AppId = OptionResolver.Resolve(result, OptionFactory.AppId, config?.AppId ?? 0),
            ApiToken = OptionResolver.Resolve(result, OptionFactory.ApiToken, config?.ApiToken),

            // RestoreRequest
            BackupDir = OptionResolver.Resolve(result, OptionFactory.BackupDir, config?.BackupDirectory),
            Mode = OptionResolver.Resolve(result, OptionFactory.Mode, config?.Mode ?? Core.Enums.RestoreMode.Upsert),
            RestoreFiles = OptionResolver.Resolve(result, OptionFactory.RestoreFiles, config?.RestoreFiles ?? false),
            Force = OptionResolver.Resolve(result, OptionFactory.Force, config?.Force ?? false),
            DryRun = OptionResolver.Resolve(result, OptionFactory.DryRun, config?.DryRun ?? false),
            ValidateOnly = OptionResolver.Resolve(result, OptionFactory.ValidateOnly, config?.ValidateOnly ?? false),
            BatchSize = OptionResolver.Resolve(result, OptionFactory.BatchSize, config?.BatchSize ?? KintoneConstants.KintoneLimit),
            // Output settings
            ColorMode = OptionResolver.Resolve(result, OptionFactory.ColorMode, config?.ColorMode ?? Core.Enums.ColorMode.Auto),
            JsonOutput = OptionResolver.Resolve(result, OptionFactory.JsonOutput, config?.JsonOutput ?? false)
        };
    }
}