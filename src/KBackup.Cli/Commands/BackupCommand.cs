using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;

namespace KBackup.Cli.Commands;

public class BackupCommand : Command {
    public BackupCommand(IHost host) : base("backup", "Perform backup of kintone application data") {
        // Define options
        var subDomainOption = new Option<string>("--sub-domain") {
            Required = true,
            Description = "Kintone sub-domain"
        };

        var appIdOption = new Option<int>("--app-id") {
            Required = true,
            Description = "Kintone application ID"
        };

        var apiToken = new Option<string>("--api-token") {
            Required = false,
            Description = "Kintone API token"
        };

        var outDirOption = new Option<string?>("--out-dir") {
            Description = "Output directory for backup files"
        };

        // Add options to the command
        this.Add(subDomainOption);
        this.Add(appIdOption);
        this.Add(apiToken);
        this.Add(outDirOption);

        this.SetAction(async parseResult => {
            var subDomain = parseResult.GetValue(subDomainOption);
            var appId = parseResult.GetValue(appIdOption);
            var apiTokenValue = parseResult.GetValue(apiToken);
            var outDir = parseResult.GetValue(outDirOption);

            // Handler logic goes here
            Console.WriteLine($"Backing up app {appId} from {subDomain} to {outDir}");
        });
    }
}