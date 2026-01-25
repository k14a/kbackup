using KBackup.Core.Enums;
using KintoneNetLibrary.Backup.Domain.Enums;
using KintoneNetLibrary.Domain.Common;

namespace KBackup.Cli.Config;

public sealed class KBackupConfig {
    public string SubDomain { get; set; } = string.Empty;
    public string ApiToken { get; set; } = string.Empty;
    public int AppId { get; set; } = 0;
    #region <<for BackupRequest>>
    public string OutputDirectory { get; set; } = string.Empty;
    public bool DownloadFiles { get; set; } = true;
    public bool IncludeFieldSchema { get; set; } = true;
    public bool Overwrite { get; set; } = false;
    public RestoreMode Mode { get; set; } = RestoreMode.Upsert;
    public bool Force { get; set; } = false;
    public bool RestoreFiles { get; set; } = false;
    public bool Pretty { get; set; } = false;
    public bool EscapeUnicode { get; set; } = true;
    public string Query { get; set; } = string.Empty;
    public int SplitSize { get; set; } = 1000;
    #endregion
    #region <<for RestoreRequest>>
    public string? BackupDirectory { get; set; }
    public bool DryRun { get; set; } = false;
    public bool ValidateOnly { get; set; } = false;
    public int BatchSize { get; set; } = KintoneConstants.KintoneLimit;
    #endregion
    public ColorMode ColorMode { get; set; } = ColorMode.Auto;
    public bool JsonOutput { get; set; } = false;
}