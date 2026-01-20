namespace KBackup.Core.Enums;

/// <summary>
/// 色付き出力モード
/// </summary>
public enum ColorMode {
    /// <summary>
    /// 端末がTTY対応の場合に色付き出力を行う(デフォルト)
    /// </summary>
    Auto,
    /// <summary>
    /// 常に色付き出力を行う
    /// </summary>
    Always,
    /// <summary>
    /// 色付き出力を行わない
    /// </summary>
    Never
}