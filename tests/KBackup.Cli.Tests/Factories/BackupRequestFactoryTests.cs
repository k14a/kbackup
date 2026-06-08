using KBackup.Cli.Factories;
using Xunit;
using KBackup.Cli.Options;
using KBackup.Core.Enums;

namespace KBackup.Cli.Tests.Factories;

public class BackupRequestFactoryTests {
    private static BackupOptions CreateValidOptions(
        string? outputDirectory = null,
        int splitSize = 1000,
        IList<string>? fieldCodes = null,
        int? batchSize = null
    ) => new() {
        SubDomain = "example",
        AppId = 1,
        ApiToken = "token",
        OutputDirectory = outputDirectory ?? Path.GetTempPath(),
        DownloadFiles = true,
        IncludeFieldSchema = true,
        Overwrite = false,
        Pretty = false,
        EscapeUnicode = true,
        Query = null,
        SplitSize = splitSize,
        FieldCodes = fieldCodes,
        BatchSize = batchSize,
        ColorMode = ColorMode.Auto,
        JsonOutput = false
    };

    [Fact]
    public void Create_正常なオプションでBackupRequestを生成できる() {
        var opts = CreateValidOptions();

        var result = BackupRequestFactory.Create(opts);

        Assert.Equal("example", result.SubDomain);
        Assert.Equal(1, result.AppId);
        Assert.Equal("token", result.ApiToken);
        Assert.Equal(1000, result.SplitSize);
    }

    [Fact]
    public void Create_OutputDirectoryが存在しない場合_DirectoryNotFoundExceptionをスローする() {
        var opts = CreateValidOptions(outputDirectory: "/nonexistent/path_xyz_12345");

        Assert.Throws<DirectoryNotFoundException>(() => BackupRequestFactory.Create(opts));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(5001)]
    public void Create_SplitSizeが範囲外の場合_ArgumentOutOfRangeExceptionをスローする(int splitSize) {
        var opts = CreateValidOptions(splitSize: splitSize);

        Assert.Throws<ArgumentOutOfRangeException>(() => BackupRequestFactory.Create(opts));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(1000)]
    [InlineData(5000)]
    public void Create_SplitSizeが境界値の場合_正常にBackupRequestを生成できる(int splitSize) {
        var opts = CreateValidOptions(splitSize: splitSize);

        var result = BackupRequestFactory.Create(opts);

        Assert.Equal(splitSize, result.SplitSize);
    }

    [Fact]
    public void Create_FieldCodesとBatchSizeを引き継ぐ() {
        var opts = CreateValidOptions(fieldCodes: ["field1", "field2"], batchSize: 200);

        var result = BackupRequestFactory.Create(opts);

        Assert.Equal(["field1", "field2"], result.FieldCodes);
        Assert.Equal(200, result.BatchSize);
    }
}
