using KBackup.Core;
using KBackup.Core.Interfaces;
using KBackup.Core.Logging;
using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;
using KintoneNetLibrary.Backup.Application.Interfaces;
using KintoneNetLibrary.Backup.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace KBackup.Infrastructure;

public partial class KintoneRepository {
    public async Task<RestoreResult> RestoreAsync(RestoreRequest request) {
        StaticLogMessages.MethodStart(this._logger, message: $"KintoneRepository.RestoreAsync started. App={request.AppId}");

        var result = new RestoreResult();

        try {
            // RestoreService のオプションをセット
            var options = new RestoreOptions {
                SubDomain = request.SubDomain,
                AppID = request.AppId,
                ApiToken = request.ApiToken,
                BackupJsonPath = Path.Combine(request.BackupDirectory.FullName, "backup.json"),
                Mode = request.Mode,
                RestoreFiles = request.RestoreFiles,
                Force = request.Force
            };

            // RestoreService にオプションを注入
            this._restoreService.Options = options;

            return await this._restoreService.RunRestoreAsync();

        } catch (Exception ex) {
            result.Success = false;
            result.Errors.Add(ex.Message);
            StaticLogMessages.MethodError(this._logger, ex, message: $"KintoneRepository.RestoreAsync failed. App={request.AppId}");
            return result;

        } finally {
            StaticLogMessages.MethodFinish(this._logger, message: $"KintoneRepository.RestoreAsync finished. App={request.AppId}");
        }
    }
}
