namespace KBackup.Core.Models;

public abstract class BaseKintoneRequest {
    public required string SubDomain { get; init; }
    public required int AppId { get; init; }
    public required string ApiToken { get; init; }
}