using Spectre.Console;
using System.Text.Json;
using KBackup.Core.Enums;
using KBackup.Core.Interfaces;

namespace KBackup.Cli.Utils;

/// <summary>
/// CommandUtils クラスは、コマンドの実行結果を表示するためのユーティリティクラスです。コマンドの成功、部分的な成功、失敗を色付きで表示したり、JSON形式で出力する機能を提供します。
/// </summary>
public static class CommandUtils {
    /// <summary>
    /// ShouldUseColor メソッドは、指定された ColorMode に基づいて、コマンドライン出力に色を使用するかどうかを判断します。ColorMode が Always の場合は常に色を使用し、Never の場合は色を使用せず、Auto の場合は出力がリダイレクトされていない場合にのみ色を使用します。
    /// </summary>
    /// <param name="mode">色の使用モードを指定する ColorMode 列挙型の値。</param>
    /// <returns>色を使用する場合は true、使用しない場合は false を返します。</returns>
    public static bool ShouldUseColor(ColorMode mode) {
        return mode switch {
            ColorMode.Always => true,
            ColorMode.Never => false,
            ColorMode.Auto => !Console.IsOutputRedirected,
            _ => false
        };
    }

    /// <summary>
    /// PrintJson メソッドは、指定された IOperationResult オブジェクトを JSON 形式でシリアライズしてコンソールに出力します。pretty パラメーターが true の場合はインデントされた形式で出力し、false の場合はコンパクトな形式で出力します。escapeUnicode パラメーターが true の場合は Unicode 文字をエスケープし、false の場合はエスケープせずに出力します。
    /// </summary>
    /// <param name="result">JSON 形式で出力する IOperationResult オブジェクト。</param>
    /// <param name="pretty">インデントされた形式で出力するかどうかを指定するオプション。</param>
    /// <param name="escapeUnicode">Unicode 文字をエスケープするかどうかを指定するオプション。</param>
    public static void PrintJson(IOperationResult result, bool? pretty = null, bool? escapeUnicode = null) {
        var json = JsonSerializer.Serialize(
            result,
            new JsonSerializerOptions {
                WriteIndented = pretty ?? false,
                Encoder = escapeUnicode ?? true
                    ? null
                    : System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }
        );

        Console.WriteLine(json);
    }

    /// <summary>
    /// PrintResult メソッドは、指定された IOperationResult オブジェクトの実行結果をコンソールに出力します。color パラメーターが true の場合は色付きで出力し、false の場合は色なしで出力します。
    /// </summary>
    /// <param name="result">出力する IOperationResult オブジェクト。</param>
    /// <param name="color">色付きで出力するかどうかを指定するオプション。</param>
    public static void PrintResult(IOperationResult result, bool color, string operationName) {
        if (!color) {
            Console.WriteLine(result.Success
                ? $"{operationName} completed successfully."
                : $"{operationName} failed.");

            if (result.IsPartialSuccess) {
                Console.WriteLine($"{operationName} completed with warnings.");
            }

            foreach (var w in result.Warnings) {
                Console.WriteLine($"WARNING: {w}");
            }

            foreach (var e in result.Errors) {
                Console.WriteLine($"ERROR: {e}");
            }

            return;
        }

        if (!result.Success) {
            AnsiConsole.MarkupLine($"[red]{operationName} failed[/]");
        } else if (result.IsPartialSuccess) {
            AnsiConsole.MarkupLine($"[yellow]{operationName} completed with warnings[/]");
        } else {
            AnsiConsole.MarkupLine($"[green]{operationName} completed successfully[/]");
        }

        foreach (var w in result.Warnings) {
            AnsiConsole.MarkupLine($"[yellow]WARNING:[/] {w}");
        }

        foreach (var e in result.Errors) {
            AnsiConsole.MarkupLine($"[red]ERROR:[/] {e}");
        }
    }
}
