namespace KBackup.Cli.Utils;

/// <summary>
/// PathHelper クラスは、バックアップやリストアの出力ディレクトリを構築するためのユーティリティクラスです。指定された出力ディレクトリとアプリIDに基づいて、適切なディレクトリパスを生成し、必要に応じてディレクトリを作成します。
/// </summary>
public static class PathHelper {
    /// <summary>
    /// BuildOutputPath メソッドは、指定された出力ディレクトリとアプリIDに基づいて、適切なディレクトリパスを生成します。出力ディレクトリが指定されていない場合は、カレントディレクトリが使用されます。生成されたパスは、"app_{appId}" という形式のサブディレクトリになります。必要に応じて、サブディレクトリが作成されます。
    /// </summary>
    /// <param name="outDir">出力ディレクトリのパス。null または空文字の場合はカレントディレクトリが使用されます。</param>
    /// <param name="appId">アプリケーションのID。</param>
    /// <returns>生成されたディレクトリの DirectoryInfo オブジェクト。</returns>
    public static DirectoryInfo BuildOutputPath(string? outDir, int appId) {
        var baseDir = string.IsNullOrEmpty(outDir) ? Directory.GetCurrentDirectory() : outDir;
        var appDir = Path.Combine(baseDir, $"app_{appId}");
        var di = new DirectoryInfo(appDir);
        if (!di.Exists) { di.Create(); }
        return di;
    }
}