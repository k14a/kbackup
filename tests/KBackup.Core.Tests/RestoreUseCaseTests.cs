using KBackup.Core.Enums;
using Xunit;
using KBackup.Core.Interfaces;
using KBackup.Core.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace KBackup.Core.Tests;

public class RestoreUseCaseTests {
    private readonly Mock<IRestoreKintoneRepository> _repositoryMock;
    private readonly RestoreUseCase _sut;

    public RestoreUseCaseTests() {
        this._repositoryMock = new Mock<IRestoreKintoneRepository>();
        this._sut = new RestoreUseCase(
            this._repositoryMock.Object,
            NullLogger<RestoreUseCase>.Instance
        );
    }

    private static RestoreRequest CreateRequest() => new() {
        SubDomain = "example",
        AppId = 1,
        ApiToken = "token",
        BackupDirectory = new DirectoryInfo(Path.GetTempPath()),
        Mode = RestoreMode.Upsert,
        BatchSize = 100
    };

    [Fact]
    public async Task RunRestoreAsync_リポジトリを必ず1回呼び出す() {
        this._repositoryMock
            .Setup(r => r.RestoreAsync(It.IsAny<RestoreRequest>()))
            .ReturnsAsync(new RestoreResult { Success = true });

        await this._sut.RunRestoreAsync(CreateRequest());

        this._repositoryMock.Verify(r => r.RestoreAsync(It.IsAny<RestoreRequest>()), Times.Once);
    }

    [Fact]
    public async Task RunRestoreAsync_リポジトリの戻り値をそのまま返す() {
        var expected = new RestoreResult { Success = true, AddedRecords = 10 };
        this._repositoryMock
            .Setup(r => r.RestoreAsync(It.IsAny<RestoreRequest>()))
            .ReturnsAsync(expected);

        var result = await this._sut.RunRestoreAsync(CreateRequest());

        Assert.Same(expected, result);
    }

    [Fact]
    public async Task RunRestoreAsync_成功の場合_Successがtrueを返す() {
        this._repositoryMock
            .Setup(r => r.RestoreAsync(It.IsAny<RestoreRequest>()))
            .ReturnsAsync(new RestoreResult { Success = true });

        var result = await this._sut.RunRestoreAsync(CreateRequest());

        Assert.True(result.Success);
        Assert.False(result.IsPartialSuccess);
    }

    [Fact]
    public async Task RunRestoreAsync_部分成功の場合_IsPartialSuccessがtrueを返す() {
        this._repositoryMock
            .Setup(r => r.RestoreAsync(It.IsAny<RestoreRequest>()))
            .ReturnsAsync(new RestoreResult { Success = true, Warnings = ["一部ファイルのアップロードに失敗"] });

        var result = await this._sut.RunRestoreAsync(CreateRequest());

        Assert.True(result.Success);
        Assert.True(result.IsPartialSuccess);
    }

    [Fact]
    public async Task RunRestoreAsync_失敗の場合_Successがfalseを返す() {
        this._repositoryMock
            .Setup(r => r.RestoreAsync(It.IsAny<RestoreRequest>()))
            .ReturnsAsync(new RestoreResult { Success = false, Errors = ["接続エラー"] });

        var result = await this._sut.RunRestoreAsync(CreateRequest());

        Assert.False(result.Success);
    }
}
