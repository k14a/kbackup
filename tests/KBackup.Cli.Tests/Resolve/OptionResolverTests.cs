using System.CommandLine;
using Xunit;
using KBackup.Cli.Resolve;

namespace KBackup.Cli.Tests.Resolve;

public class OptionResolverTests {
    /// <summary>
    /// ParseResult のヘルパー。指定した args で option を持つコマンドをパースして返す。
    /// </summary>
    private static ParseResult Parse<T>(Option<T> option, string[] args) {
        var command = new RootCommand { option };
        return command.Parse(args);
    }

    [Fact]
    public void Resolve_CLIで値が指定された場合_CLI値を返す() {
        var option = new Option<string?>("--foo");
        var result = Parse(option, ["--foo", "cli-value"]);

        var actual = OptionResolver.Resolve(result, option, "config-value");

        Assert.Equal("cli-value", actual);
    }

    [Fact]
    public void Resolve_CLI未指定でConfig値がある場合_Config値を返す() {
        var option = new Option<string?>("--foo");
        var result = Parse(option, []);

        var actual = OptionResolver.Resolve(result, option, "config-value");

        Assert.Equal("config-value", actual);
    }

    [Fact]
    public void Resolve_CLI未指定でConfig値もない場合_オプションデフォルト値を返す() {
        var option = new Option<string?>("--foo") { DefaultValueFactory = _ => "option-default" };
        var result = Parse(option, []);

        var actual = OptionResolver.Resolve<string?>(result, option, null);

        Assert.Equal("option-default", actual);
    }

    [Fact]
    public void Resolve_CLI値はConfigよりも優先される() {
        var option = new Option<string?>("--foo");
        var result = Parse(option, ["--foo", "cli-value"]);

        var actual = OptionResolver.Resolve(result, option, "config-value");

        Assert.Equal("cli-value", actual);
        Assert.NotEqual("config-value", actual);
    }

    [Fact]
    public void Resolve_Config値はオプションデフォルトよりも優先される() {
        var option = new Option<string?>("--foo") { DefaultValueFactory = _ => "option-default" };
        var result = Parse(option, []);

        var actual = OptionResolver.Resolve<string?>(result, option, "config-value");

        Assert.Equal("config-value", actual);
        Assert.NotEqual("option-default", actual);
    }
}
