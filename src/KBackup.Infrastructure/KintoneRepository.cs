using KBackup.Core.Interfaces;
using KintoneNetLibrary.Backup.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace KBackup.Infrastructure;

/// <summary>
/// KintoneRepository クラスは、IKintoneRepository インターフェースを実装するクラスであり、Kintone のバックアップおよびリストア処理を実行するためのリポジトリクラスです。このクラスは、KintoneBackupService を使用してバックアップおよびリストア処理を実行し、ILogger を使用して処理の開始と終了、および処理結果に応じたログメッセージを出力します。
/// </summary>
/// <param name="backupService">バックアップ処理を実行するための IBackupService インスタンス</param>
/// <param name="restoreService">リストア処理を実行するための IRestoreService インスタンス</param>
/// <param name="logger">ログ出力を行うための ILogger インスタンス</param>
public partial class KintoneRepository(IBackupService backupService, IRestoreService restoreService, ILogger<KintoneRepository> logger) : IKintoneRepository {

    private readonly IBackupService _backupService = backupService;
    private readonly IRestoreService _restoreService = restoreService;
    private readonly ILogger<KintoneRepository> _logger = logger;
}
