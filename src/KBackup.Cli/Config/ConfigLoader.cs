using System.Text.Json;

namespace KBackup.Cli.Config;

public static class ConfigLoader {
    private const string DefaultFileName = "kbackup.config.json";

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

    private static KBackupConfig LoadFile(string path) {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<KBackupConfig>(json)
               ?? throw new InvalidOperationException("Invalid config file format.");
    }
}