using KBackup.Cli.Commands;
using KBackup.Cli.Options;
using KBackup.Core.Models;
using KintoneNetLibrary.Domain.Common;

namespace KBackup.Cli.Factories;

public static class RestoreRequestFactory {
    public static RestoreRequest Create(RestoreOptions opts) {
        // --- 1. BackupDir の存在チェック ---
        var dir = new DirectoryInfo(opts.BackupDir);
        if (!dir.Exists) {
            throw new DirectoryNotFoundException($"Backup directory not found: {dir.FullName}");
        }

        // --- 2. manifest.json の読み込み（必要なら） ---
        // ※ ここではスケルトンなので未実装
        // var manifest = ManifestLoader.Load(dir);

        // --- 3. オプションの整合性チェック ---
        ValidateConsistency(opts);

        // --- 4. RestoreRequest の構築 ---
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

    private static void ValidateConsistency(RestoreOptions opts) {
        // --- ValidateOnly と DryRun の矛盾 ---
        if (opts.ValidateOnly && opts.DryRun) {
            throw new ArgumentException("Cannot specify both --validate-only and --dry-run.");
        }

        // --- BatchSize の範囲チェック ---
        if (opts.BatchSize <= 0 || opts.BatchSize > KintoneConstants.KintoneLimit) {
            var paramName = nameof(opts.BatchSize);
            throw new ArgumentOutOfRangeException(paramName, $"BatchSize must be between 1 and {KintoneConstants.KintoneLimit}.");
        }

        // --- Force の扱い（必要なら追加） ---
        // if (opts.Force && opts.Mode == RestoreMode.FullReplace) { ... }
    }
}