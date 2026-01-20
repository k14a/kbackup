namespace KBackup.Core;

public class KintoneBackupData {
    public string AppName { get; set; } = "";
    public object Schema { get; set; } = default!;
    public object Records { get; set; } = default!;
    public List<KintoneFileData> Files { get; set; } = new();
}

public class KintoneFileData {
    public string FileKey { get; set; } = "";
    public string FileName { get; set; } = "";
    public byte[] Content { get; set; } = Array.Empty<byte>();
}
