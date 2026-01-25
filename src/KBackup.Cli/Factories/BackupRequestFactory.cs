using KBackup.Cli.Options;
using KBackup.Core.Models;

namespace KBackup.Cli.Factories;

public static class BackupRequestFactory {
    public static BackupRequest Create(BackupOptions opts) {
        // --- 1. OutputDir の存在チェック ---
        var dir = new DirectoryInfo(opts.OutputDir);
        if (!dir.Exists) {
            throw new DirectoryNotFoundException($"Output directory not found: {dir.FullName}");
        }

        // --- 2. オプションの整合性チェック ---
        ValidateConsistency(opts);

        // --- 3. BackupRequest の生成 ---
        return new BackupRequest {
            SubDomain = opts.SubDomain,
            AppId = opts.AppId,
            ApiToken = opts.ApiToken,
            OutputDirectory = dir,
            DownloadFiles = opts.DownloadFiles,
            IncludeFieldSchema = opts.IncludeFieldSchema,
            Overwrite = opts.Overwrite,
            Pretty = opts.Pretty,
            EscapeUnicode = opts.EscapeUnicode,
            Query = opts.Query,
            SplitSize = opts.SplitSize
        };
    }

    private static void ValidateConsistency(BackupOptions opts) {
        // split size の範囲チェック
        if (opts.SplitSize <= 0 || opts.SplitSize > 5000) {
            var paramName = nameof(opts.SplitSize);
            throw new ArgumentOutOfRangeException(paramName, "SplitSize must be between 1 and 5000.");
        }
    }
}