using KBackup.Core.Models;
using KintoneNetLibrary.Backup.Application.DTOs;

namespace KBackup.Core.Interfaces;

public interface IRestoreUseCase {
    Task<RestoreResult> RunRestoreAsync(RestoreRequest request);
}
