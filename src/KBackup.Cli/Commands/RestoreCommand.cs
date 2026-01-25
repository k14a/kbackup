using KBackup.Cli.Factories;
using KBackup.Cli.Resolve;
using KBackup.Cli.Utils;
using KBackup.Core.Enums;
using KBackup.Core.Interfaces;
using KintoneNetLibrary.Backup.Domain.Enums;
using KintoneNetLibrary.Domain.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KBackup.Cli.Commands;

public class RestoreCommand : BaseCommand {
    public RestoreCommand(IHost host, ILogger<RestoreCommand> logger)
        : base("restore", "Restore Kintone application data from backup", logger) {

        this.SetAction(async parseResult => {
            var config = this.LoadConfig(parseResult);
            var opts = RestoreOptionsResolver.Resolve(parseResult, config);
            var request = RestoreRequestFactory.Create(opts);

            var useCase = host.Services.GetRequiredService<IRestoreUseCase>();
            var result = await useCase.RunRestoreAsync(request);

            if (opts.JsonOutput) {
                CommandUtils.PrintJson(result);
                return;
            }

            var useColor = CommandUtils.ShouldUseColor(opts.ColorMode);
            CommandUtils.PrintResult(result, useColor);
        });
    }
}
