namespace KBackup.Cli.Utils;

public static class PathHelper {
    public static DirectoryInfo BuildOutputPath(string? outDir, int appId) {
        var baseDir = string.IsNullOrEmpty(outDir) ? Directory.GetCurrentDirectory() : outDir;
        var appDir = Path.Combine(baseDir, $"app_{appId}");
        var di = new DirectoryInfo(appDir);
        if (!di.Exists) { di.Create(); }
        return di;
    }
}