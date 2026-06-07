using KBackup.Cli.Options;
using KBackup.Core.Models;
using static KintoneNetLibrary.Domain.Common.KintoneConstants;

namespace KBackup.Cli.Factories;

/// <summary>
/// RestoreRequestFactory は、RestoreOptions から RestoreRequest を構築するためのファクトリークラスです。
/// </summary>
public static class RestoreRequestFactory {
    /// <summary>
    /// Create メソッドは、RestoreOptions を受け取り、必要な検証と処理を行った上で RestoreRequest を生成します。
    /// </summary>
    /// <param name="opts">RestoreOptions オブジェクト</param>
    /// <returns>RestoreRequest オブジェクト</returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static RestoreRequest Create(RestoreOptions opts) {
        // --- 1. BackupDir の存在チェック ---
        if (string.IsNullOrEmpty(opts.BackupDir)) {
            throw new ArgumentException("--backup-dir は必須です。");
        }
        var dir = new DirectoryInfo(opts.BackupDir);
        if (!dir.Exists) {
            throw new DirectoryNotFoundException($"Backup directory not found: {dir.FullName}");
        }

        // --- 2. オプションの整合性チェック ---
        ValidateConsistency(opts);

        // --- 3. RestoreRequest の構築 ---
        return new RestoreRequest {
            SubDomain = opts.SubDomain,
            AppId = opts.AppId,
            ApiToken = opts.ApiToken,
            BackupDirectory = dir,
            Mode = opts.Mode,
            RestoreFiles = opts.RestoreFiles,
            Force = opts.Force,
            DryRun = opts.DryRun,
            ValidateOnly = opts.ValidateOnly,
            BatchSize = opts.BatchSize,
            ColorMode = opts.ColorMode,
            JsonOutput = opts.JsonOutput
        };
    }

    /// <summary>
    /// ValidateConsistency メソッドは、RestoreOptions のオプション間の矛盾をチェックします。
    /// </summary>
    /// <param name="opts">RestoreOptions オブジェクト</param>
    /// <exception cref="ArgumentException">ValidateOnly と DryRun の両方が指定された場合にスローされます。</exception>
    /// <exception cref="ArgumentOutOfRangeException">BatchSize が範囲外の場合にスローされます。</exception>
    private static void ValidateConsistency(RestoreOptions opts) {
        // --- ValidateOnly と DryRun の矛盾 ---
        if (opts.ValidateOnly && opts.DryRun) {
            throw new ArgumentException("Cannot specify both --validate-only and --dry-run.");
        }

        // --- BatchSize の範囲チェック ---
        if (opts.BatchSize <= 0 || opts.BatchSize > KintoneLimit) {
            var paramName = nameof(opts.BatchSize);
            throw new ArgumentOutOfRangeException(paramName, $"BatchSize must be between 1 and {KintoneLimit}.");
        }

    }
}