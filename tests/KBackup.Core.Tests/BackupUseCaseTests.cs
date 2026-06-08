using KBackup.Core.Interfaces;
using Xunit;
using KBackup.Core.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace KBackup.Core.Tests;

public class BackupUseCaseTests {
    private readonly Mock<IBackupKintoneRepository> _repositoryMock;
    private readonly BackupUseCase _sut;

    public BackupUseCaseTests() {
        this._repositoryMock = new Mock<IBackupKintoneRepository>();
        this._sut = new BackupUseCase(
            this._repositoryMock.Object,
            NullLogger<BackupUseCase>.Instance
        );
    }

    private static BackupRequest CreateRequest() => new() {
        SubDomain = "example",
        AppId = 1,
        ApiToken = "token",
        OutputDirectory = new DirectoryInfo(Path.GetTempPath())
    };

    [Fact]
    public async Task RunBackupAsync_リポジトリを必ず1回呼び出す() {
        this._repositoryMock
            .Setup(r => r.BackupAsync(It.IsAny<BackupRequest>()))
            .ReturnsAsync(new BackupResult { Success = true });

        await this._sut.RunBackupAsync(CreateRequest());

        this._repositoryMock.Verify(r => r.BackupAsync(It.IsAny<BackupRequest>()), Times.Once);
    }

    [Fact]
    public async Task RunBackupAsync_リポジトリの戻り値をそのまま返す() {
        var expected = new BackupResult { Success = true, RecordCount = 42 };
        this._repositoryMock
            .Setup(r => r.BackupAsync(It.IsAny<BackupRequest>()))
            .ReturnsAsync(expected);

        var result = await this._sut.RunBackupAsync(CreateRequest());

        Assert.Same(expected, result);
    }

    [Fact]
    public async Task RunBackupAsync_成功の場合_Successがtrueを返す() {
        this._repositoryMock
            .Setup(r => r.BackupAsync(It.IsAny<BackupRequest>()))
            .ReturnsAsync(new BackupResult { Success = true });

        var result = await this._sut.RunBackupAsync(CreateRequest());

        Assert.True(result.Success);
        Assert.False(result.IsPartialSuccess);
    }

    [Fact]
    public async Task RunBackupAsync_部分成功の場合_IsPartialSuccessがtrueを返す() {
        this._repositoryMock
            .Setup(r => r.BackupAsync(It.IsAny<BackupRequest>()))
            .ReturnsAsync(new BackupResult { Success = true, Warnings = ["一部ファイルのダウンロードに失敗"] });

        var result = await this._sut.RunBackupAsync(CreateRequest());

        Assert.True(result.Success);
        Assert.True(result.IsPartialSuccess);
    }

    [Fact]
    public async Task RunBackupAsync_失敗の場合_Successがfalseを返す() {
        this._repositoryMock
            .Setup(r => r.BackupAsync(It.IsAny<BackupRequest>()))
            .ReturnsAsync(new BackupResult { Success = false, Errors = ["接続エラー"] });

        var result = await this._sut.RunBackupAsync(CreateRequest());

        Assert.False(result.Success);
    }
}
