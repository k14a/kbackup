using System.Text.Json;

namespace KBackup.Cli.Config;

/// <summary>
/// KBackupの設定ファイルを読み込むためのユーティリティクラス。
/// </summary>
public static class ConfigLoader {
    private const string DefaultFileName = "kbackup.config.json";

    /// <summary>
    /// 指定されたパス、またはカレントディレクトリのデフォルトファイルからKBackupConfigを読み込む。
    /// </summary>
    /// <param name="path">設定ファイルのパス</param>
    /// <returns>読み込まれたKBackupConfigオブジェクト、または <c>null</c></returns>
    /// <exception cref="FileNotFoundException">指定されたファイルが存在しない場合にスローされる</exception>
    public static KBackupConfig? Load(string? path) {
        // 1. 明示的に指定された場合
        if (!string.IsNullOrWhiteSpace(path)) {
            if (!File.Exists(path)) {
                throw new FileNotFoundException($"Config file not found: {path}");
            }

            return LoadFile(path);
        }

        // 2. カレントディレクトリのデフォルトファイルを探す
        var defaultPath = Path.Combine(Environment.CurrentDirectory, DefaultFileName);

        if (File.Exists(defaultPath)) {
            return LoadFile(defaultPath);
        }

        // 3. 見つからなければ null
        return null;
    }

    /// <summary>
    /// 指定されたファイルパスからKBackupConfigを読み込む。
    /// </summary>
    /// <param name="path">設定ファイルのパス</param>
    /// <returns>読み込まれたKBackupConfigオブジェクト</returns>
    /// <exception cref="InvalidOperationException">設定ファイルの形式が無効な場合にスローされる</exception>
    private static KBackupConfig LoadFile(string path) {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<KBackupConfig>(json)
               ?? throw new InvalidOperationException("Invalid config file format.");
    }
}