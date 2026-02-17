using System.CommandLine;
using KBackup.Cli.Commands;
using KBackup.Cli.Config;
using KBackup.Cli.Factories;
using KBackup.Cli.Options;
using KintoneNetLibrary.Backup.Domain.Enums;
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
            SubDomain = BaseCommand.Resolve(result, OptionFactory.SubDomain, config?.SubDomain),
            AppId = BaseCommand.Resolve(result, OptionFactory.AppId, config?.AppId ?? 0),
            ApiToken = BaseCommand.Resolve(result, OptionFactory.ApiToken, config?.ApiToken),

            // RestoreRequest
            BackupDir = BaseCommand.Resolve(result, OptionFactory.BackupDir, config?.BackupDirectory),
            Mode = BaseCommand.Resolve(result, OptionFactory.Mode, config?.Mode ?? RestoreMode.Upsert),
            RestoreFiles = BaseCommand.Resolve(result, OptionFactory.RestoreFiles, config?.RestoreFiles ?? false),
            Force = BaseCommand.Resolve(result, OptionFactory.Force, config?.Force ?? false),
            DryRun = BaseCommand.Resolve(result, OptionFactory.DryRun, config?.DryRun ?? false),
            ValidateOnly = BaseCommand.Resolve(result, OptionFactory.ValidateOnly, config?.ValidateOnly ?? false),
            BatchSize = BaseCommand.Resolve(result, OptionFactory.BatchSize, config?.BatchSize ?? KintoneConstants.KintoneLimit),
            // Output settings
            ColorMode = BaseCommand.Resolve(result, OptionFactory.OutputColorMode, config?.ColorMode ?? Core.Enums.ColorMode.Auto),
            JsonOutput = BaseCommand.Resolve(result, OptionFactory.JsonOutput, config?.JsonOutput ?? false)
        };
    }
}