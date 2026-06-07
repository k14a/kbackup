namespace KBackup.Core.Enums;

/// <summary>
/// リストアモード。
/// </summary>
public enum RestoreMode {
    /// <summary>既存レコードを全て削除し、バックアップデータのみを復元する。</summary>
    FullReplace,
    /// <summary>バックアップデータのレコード ID が存在する場合は更新、存在しない場合は作成する。</summary>
    Upsert,
    /// <summary>既存レコードを変更せず、バックアップデータのレコード ID が存在しない場合のみ作成する。</summary>
    Merge,
}
