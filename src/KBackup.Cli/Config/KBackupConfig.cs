using KintoneNetLibrary.Backup.Domain.Enums;

namespace KBackup.Cli.Config;

public sealed class KBackupConfig {
    public string? SubDomain { get; set; }
    public string? ApiToken { get; set; }
    public int? AppId { get; set; }
    #region <<for BackupRequest>>
    public string? OutputDirectory { get; set; }
    public bool? DownloadFiles { get; set; }
    public bool? IncludeFieldSchema { get; set; }
    public bool? Overwrite { get; set; }
    public RestoreMode Mode { get; set; } = RestoreMode.Upsert;
    public bool? Force { get; set; }
    public bool? RestoreFiles { get; set; }
    public bool? Pretty { get; set; }
    public bool? EscapeUnicode { get; set; }
    #endregion
    #region <<for RestoreRequest>>
    public string? BackupDirectory { get; set; }
    public bool? DryRun { get; set; }
    public bool? ValidateOnly { get; set; }
    public int? BatchSize { get; set; }
    #endregion
}