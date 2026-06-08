using KBackup.Cli.Factories;
using Xunit;
using KBackup.Cli.Options;
using KBackup.Core.Enums;

namespace KBackup.Cli.Tests.Factories;

public class RestoreRequestFactoryTests {
    private static RestoreOptions CreateValidOptions(
        string? backupDir = null,
        bool validateOnly = false,
        bool dryRun = false,
        int batchSize = 100
    ) => new() {
        SubDomain = "example",
        AppId = 1,
        ApiToken = "token",
        BackupDir = backupDir ?? Path.GetTempPath(),
        Mode = RestoreMode.Upsert,
        RestoreFiles = true,
        Force = false,
        ValidateOnly = validateOnly,
        DryRun = dryRun,
        BatchSize = batchSize,
        ColorMode = ColorMode.Auto,
        JsonOutput = false
    };

    [Fact]
    public void Create_正常なオプションでRestoreRequestを生成できる() {
        var opts = CreateValidOptions();

        var result = RestoreRequestFactory.Create(opts);

        Assert.Equal("example", result.SubDomain);
        Assert.Equal(1, result.AppId);
        Assert.Equal("token", result.ApiToken);
        Assert.Equal(RestoreMode.Upsert, result.Mode);
    }

    [Fact]
    public void Create_BackupDirが空文字の場合_ArgumentExceptionをスローする() {
        var opts = CreateValidOptions(backupDir: "");

        Assert.Throws<ArgumentException>(() => RestoreRequestFactory.Create(opts));
    }

    [Fact]
    public void Create_BackupDirがnullの場合_ArgumentExceptionをスローする() {
        var opts = CreateValidOptions(backupDir: null);
        // CreateValidOptions の Path.GetTempPath() を回避するため直接インスタンス化する
        var nullOpts = new RestoreOptions {
            SubDomain = "example",
            AppId = 1,
            ApiToken = "token",
            BackupDir = null,
            Mode = RestoreMode.Upsert,
            RestoreFiles = true,
            Force = false,
            DryRun = false,
            ValidateOnly = false,
            BatchSize = 100,
            ColorMode = ColorMode.Auto,
            JsonOutput = false
        };

        Assert.Throws<ArgumentException>(() => RestoreRequestFactory.Create(nullOpts));
    }

    [Fact]
    public void Create_BackupDirが存在しないパスの場合_DirectoryNotFoundExceptionをスローする() {
        var opts = CreateValidOptions(backupDir: "/nonexistent/path_xyz_12345");

        Assert.Throws<DirectoryNotFoundException>(() => RestoreRequestFactory.Create(opts));
    }

    [Fact]
    public void Create_ValidateOnlyとDryRunを同時に指定した場合_ArgumentExceptionをスローする() {
        var opts = CreateValidOptions(validateOnly: true, dryRun: true);

        Assert.Throws<ArgumentException>(() => RestoreRequestFactory.Create(opts));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(101)]
    public void Create_BatchSizeが範囲外の場合_ArgumentOutOfRangeExceptionをスローする(int batchSize) {
        var opts = CreateValidOptions(batchSize: batchSize);

        Assert.Throws<ArgumentOutOfRangeException>(() => RestoreRequestFactory.Create(opts));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(100)]
    public void Create_BatchSizeが境界値の場合_正常にRestoreRequestを生成できる(int batchSize) {
        var opts = CreateValidOptions(batchSize: batchSize);

        var result = RestoreRequestFactory.Create(opts);

        Assert.Equal(batchSize, result.BatchSize);
    }
}
