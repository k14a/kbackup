using KBackup.Core.Interfaces;
using KintoneNetLibrary.Backup.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace KBackup.Infrastructure;

/// <summary>
/// Kintone のバックアップおよびリストア処理を実行するリポジトリクラス。
/// IBackupKintoneRepository および IRestoreKintoneRepository を実装する。
/// </summary>
/// <param name="backupService">バックアップ処理を実行するための IBackupService インスタンス</param>
/// <param name="restoreService">リストア処理を実行するための IRestoreService インスタンス</param>
/// <param name="logger">ログ出力を行うための ILogger インスタンス</param>
public partial class KintoneRepository(IBackupService backupService, IRestoreService restoreService, ILogger<KintoneRepository> logger) : IBackupKintoneRepository, IRestoreKintoneRepository {

    private readonly IBackupService _backupService = backupService;
    private readonly IRestoreService _restoreService = restoreService;
    private readonly ILogger<KintoneRepository> _logger = logger;
}
