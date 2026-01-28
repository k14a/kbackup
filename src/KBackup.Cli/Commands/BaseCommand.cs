using System.CommandLine;
using KBackup.Cli.Config;
using KBackup.Core.Enums;
using Microsoft.Extensions.Logging;

namespace KBackup.Cli.Commands;

/// <summary>
/// Represents the base class for commands in the KBackup CLI application.
/// </summary>
/// <remarks>
/// This class provides common options for commands, including configuration file path,
/// color output mode, and JSON output format. Derived classes should implement specific
/// command functionality.
/// </remarks>
/// <typeparam name="T">The type of the command.</typeparam>
/// <param name="name">The name of the command.</param>
/// <param name="description">A description of the command.</param>
/// <param name="logger">An instance of <see cref="ILogger{BaseCommand}"/> for logging.</param>
/// <property name="ConfigOption">
/// Gets the option for specifying the path to the configuration file.
/// </property>
/// <property name="ColorOption">
/// Gets the option for specifying the color output mode.
/// </property>
/// <property name="JsonOption">
/// Gets the option for specifying whether to output results as JSON.
/// </property>
/// <returns>
/// An instance of <see cref="KBackupConfig"/> loaded from the specified configuration path,
/// or <c>null</c> if the configuration could not be loaded.
/// </returns>
/// <param name="parseResult">The result of parsing the command line arguments.</param>
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
    public static T Resolve<T>(ParseResult result, Option<T> option, T? configValue) {
        var value = result.GetValue(option);
        if (value is not null) { return value; }
        if (configValue is not null) { return configValue; }
        return (T)option.GetDefaultValue()!;
    }
}
