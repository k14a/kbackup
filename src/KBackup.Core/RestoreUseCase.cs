using KBackup.Core.Interfaces;
using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KBackup.Core;

public class RestoreUseCase(IKintoneRepository repository, ILogger<RestoreUseCase> logger) : IRestoreUseCase {

    private readonly IKintoneRepository _repository = repository;
    private readonly ILogger<RestoreUseCase> _logger = logger;

    public async Task<RestoreResult> RunRestoreAsync(RestoreRequest request) {
        this._logger.LogInformation("Restore started. App={AppId}", request.AppId);

        var result = await this._repository.RestoreAsync(request);

        if (!result.Success) {
            this._logger.LogWarning("Restore finished with errors.");
        } else if (result.IsPartialSuccess) {
            this._logger.LogWarning("Restore finished with warnings.");
        } else {
            this._logger.LogInformation("Restore finished successfully.");
        }

        return result;
    }
}
