using System.CommandLine;
using KBackup.Cli.Factories;
using KBackup.Cli.Resolve;
using KBackup.Cli.Utils;
using KBackup.Core.Enums;
using KBackup.Core.Interfaces;
using KBackup.Core.Logging;
using KBackup.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KBackup.Cli.Commands;

/// <summary>
/// Command for performing backup of kintone application data.
/// </summary>
public class BackupCommand : BaseCommand {
    public BackupCommand(IHost host, ILogger<BackupCommand> logger)
        : base("backup", "Perform backup of kintone application data", logger) {

        // Backup用のオプションをすべて登録
        this.Add(OptionFactory.SubDomain);
        this.Add(OptionFactory.AppId);
        this.Add(OptionFactory.ApiToken);

        this.Add(OptionFactory.OutputDir);
        this.Add(OptionFactory.DownloadFiles);
        this.Add(OptionFactory.IncludeFieldSchema);
        this.Add(OptionFactory.Overwrite);
        this.Add(OptionFactory.Pretty);
        this.Add(OptionFactory.EscapeUnicode);
        this.Add(OptionFactory.Query);
        this.Add(OptionFactory.Split);

        this.Add(OptionFactory.OutputColorMode);
        this.Add(OptionFactory.JsonOutput);

        // Define options
        this.SetAction(async parseResult => {
            var config = this.LoadConfig(parseResult);
            var opts = BackupOptionsResolver.Resolve(parseResult, config);
            var request = BackupRequestFactory.Create(opts);

            // var factory = host.Services.GetRequiredService<IKintoneAccessFactory>();
            // var kintoneAccess = factory.Create(request.SubDomain, request.ApiToken);

            var useCase = host.Services.GetRequiredService<IBackupUseCase>();
            var result = await useCase.RunBackupAsync(request);

            // JSON 出力が指定された場合は最優先
            if (opts.JsonOutput) {
                CommandUtils.PrintJson(result, opts.Pretty, opts.EscapeUnicode);
                return;
            }

            // 色付き出力
            var useColor = CommandUtils.ShouldUseColor(opts.ColorMode);
            CommandUtils.PrintResult(result, useColor);
        });
    }
}