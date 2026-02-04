using KBackup.Core.Interfaces;
using KintoneNetLibrary.Backup.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace KBackup.Infrastructure;

/// <summary>
/// KintoneRepository
/// </summary>
/// <param name="backupService"></param>
/// <param name="restoreService"></param>
/// <param name="logger"></param>
public partial class KintoneRepository(IBackupService backupService, IRestoreService restoreService, ILogger<KintoneRepository> logger)
    : IKintoneRepository {

    private readonly IBackupService _backupService = backupService;
    private readonly IRestoreService _restoreService = restoreService;
    private readonly ILogger<KintoneRepository> _logger = logger;
}
