using KBackup.Core;
using KBackup.Core.Interfaces;
using KBackup.Core.Logging;
using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KBackup.Infrastructure;

public partial class KintoneRepository : IKintoneRepository {
    public async Task<BackupResult> BackupAsync(BackupRequest request) {
        StaticLogMessages.MethodStart(this._logger, message: $"KintoneRepository.RunBackupAsync started. App={request.AppId}");

        try {
            this._backupService.Options = new BackupOptions {
                SubDomain = request.SubDomain,
                AppID = request.AppId,
                ApiToken = request.ApiToken,
                OutputPath = request.OutputDirectory,
                DownloadFiles = request.DownloadFiles,
                IncludeFieldSchema = request.IncludeFieldSchema,
                Overwrite = request.Overwrite,
                Pretty = request.Pretty,
                EscapeUnicode = request.EscapeUnicode,
                Query = request.Query,
                SplitSize = request.SplitSize
            };

            return await this._backupService.RunBackupAsync();

        } finally {
            StaticLogMessages.MethodFinish(this._logger, message: $"KintoneRepository.RunBackupAsync finished. App={request.AppId}");
        }
    }
}
