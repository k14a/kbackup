using KBackup.Cli.Options;
using KBackup.Core.Models;

namespace KBackup.Cli.Factories;

/// <summary>
/// BackupRequestオブジェクトを生成するためのファクトリークラス。
/// </summary>
public static class BackupRequestFactory {
    /// <summary>
    /// BackupOptionsからBackupRequestを生成する。オプションの整合性チェックも行う。
    /// </summary>
    /// <param name="opts">BackupOptionsオブジェクト</param>
    /// <returns>生成されたBackupRequestオブジェクト</returns>
    /// <exception cref="DirectoryNotFoundException">出力ディレクトリが存在しない場合にスローされる</exception>
    public static BackupRequest Create(BackupOptions opts) {
        // --- 1. OutputDirectory の存在チェック ---
        var dir = new DirectoryInfo(opts.OutputDirectory);
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
            SplitSize = opts.SplitSize,
            FieldCodes = opts.FieldCodes,
            BatchSize = opts.BatchSize
        };
    }

    /// <summary>
    /// BackupOptionsの値の整合性をチェックする。必要に応じて例外をスローする。
    /// </summary>
    /// <param name="opts">BackupOptionsオブジェクト</param>
    /// <exception cref="ArgumentOutOfRangeException">オプションの値が範囲外の場合にスローされる</exception>
    private static void ValidateConsistency(BackupOptions opts) {
        // split size の範囲チェック
        if (opts.SplitSize <= 0 || opts.SplitSize > 5000) {
            var paramName = nameof(opts.SplitSize);
            throw new ArgumentOutOfRangeException(paramName, "SplitSize must be between 1 and 5000.");
        }
    }
}