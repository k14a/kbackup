namespace KBackup.Cli.Config;

public sealed class KBackupConfig {
    public string? SubDomain { get; set; }
    public string? ApiToken { get; set; }
    public int? AppId { get; set; }
    public string? OutputDirectory { get; set; }
}