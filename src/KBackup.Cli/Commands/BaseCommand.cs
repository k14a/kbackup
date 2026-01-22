using System.CommandLine;
using KBackup.Cli.Config;
using KBackup.Core.Enums;
using Microsoft.Extensions.Logging;

namespace KBackup.Cli.Commands;

public abstract class BaseCommand : Command {
    protected Option<string?> ConfigOption { get; }
    protected Option<ColorMode> ColorOption { get; }
    protected Option<bool> JsonOption { get; }
    protected ILogger<BaseCommand> _logger;

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

    protected KBackupConfig? LoadConfig(ParseResult parseResult) {
        var configPath = parseResult.GetValue(this.ConfigOption);
        return ConfigLoader.Load(configPath);
    }
}
