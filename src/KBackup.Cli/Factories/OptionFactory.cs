using System.CommandLine;
using KBackup.Core.Enums;
using KintoneNetLibrary.Backup.Domain.Enums;
using KintoneNetLibrary.Domain.Common;
using static KintoneNetLibrary.Domain.Common.KintoneConstants;

namespace KBackup.Cli.Factories;

/// <summary>
/// コマンドラインオプションを定義するためのファクトリークラス。各コマンドで共通して使用されるオプションもここで定義する。
/// </summary>
public static class OptionFactory {
    /// <summary>
    /// kintoneのサブドメインを指定するオプション。例: "example"（URLが "https://example.kintone.com" の場合）。必須。
    /// </summary>
    public static readonly Option<string> SubDomain =
        new("--sub-domain") {
            Required = true,
            Description = "Kintone sub-domain"
        };

    /// <summary>
    /// kintoneのアプリIDを指定するオプション。必須。
    /// </summary>
    public static readonly Option<int> AppId =
        new("--app-id") {
            Required = true,
            Description = "Kintone application ID."
        };

    /// <summary>
    /// kintoneのAPIトークンを指定するオプション。任意。
    /// </summary>
    public static readonly Option<string> ApiToken =
        new("--api-token") {
            Required = false,
            Description = "Kintone API token"
        };

    /// <summary>
    /// コマンドの出力に色を付けるかどうかを指定するオプション。Auto（自動）、Always（常に）、Never（なし）から選択可能。デフォルトはAuto。
    /// </summary>
    public static readonly Option<ColorMode> OutputColorMode =
        new("--color-mode") {
            Description = "Color output mode (Auto, Always, Never). Default is Auto.",
            DefaultValueFactory = _ => ColorMode.Auto
        };

    /// <summary>
    /// コマンドの出力をJSON形式にするかどうかを指定するオプション。デフォルトはfalse。
    /// </summary>
    public static readonly Option<bool> JsonOutput =
        new("--json") {
            Description = "Output the result in JSON format. Default is false.",
            DefaultValueFactory = _ => false
        };

    #region <<for BackupCommand>>
    /// <summary>
    /// バックアップの出力先ディレクトリを指定するオプション。必須。例: "./backups"。このディレクトリの下にAppIDや日時を含むサブディレクトリが作成され、その中にバックアップファイルが保存される。
    /// </summary>
    public static readonly Option<string> OutputDir =
        new("--out-dir") {
            Description = "Parent directory where backup folders will be created.",
            DefaultValueFactory = _ => "."
        };

    /// <summary>
    /// 添付ファイルをダウンロードするかどうかを指定するオプション。デフォルトはtrue。バックアップのサイズが大きくなる可能性があるため、必要に応じてfalseに設定することもできる。
    /// </summary>
    public static readonly Option<bool> DownloadFiles =
        new("--download-files") {
            Description = "Whether to download attached files (if any). Default is true.",
            DefaultValueFactory = _ => true
        };

    /// <summary>
    /// フィールドスキーマをバックアップに含めるかどうかを指定するオプション。デフォルトはtrue。フィールドスキーマを含めると、復元時にフィールドの型や設定を再現できるが、バックアップファイルのサイズが大きくなる可能性があるため、必要に応じてfalseに設定することもできる。
    /// </summary>
    public static readonly Option<bool> IncludeFieldSchema =
        new("--include-field-schema") {
            Description = "Whether to include field schema in the backup. Default is true.",
            DefaultValueFactory = _ => true
        };

    /// <summary>
    /// バックアップディレクトリ内の既存ファイルを上書きするかどうかを指定するオプション。デフォルトはfalse。trueに設定すると、同じAppIDと日時のサブディレクトリが既に存在する場合、その中のファイルが上書きされる可能性があるため、使用には注意が必要。
    /// </summary>
    public static readonly Option<bool> Overwrite =
        new("--overwrite") {
            Description = "Whether to overwrite existing files in the backup directory. Default is false. Use with caution.",
            DefaultValueFactory = _ => false
        };

    /// <summary>
    /// JSON出力を整形するかどうかを指定するオプション。デフォルトはfalse。trueに設定すると、JSON出力が人間に読みやすい形式（インデントや改行）で表示されるようになる。ただし、ファイルサイズが大きくなる可能性があるため、必要に応じてfalseに設定することもできる。
    /// </summary>
    public static readonly Option<bool> Pretty =
        new("--pretty") {
            Description = "Whether to pretty-print JSON output. Default is false.",
            DefaultValueFactory = _ => false
        };

    /// <summary>
    /// JSON出力でUnicode文字をエスケープするかどうかを指定するオプション。デフォルトはtrue。
    /// </summary>
    public static readonly Option<bool> EscapeUnicode =
        new("--escape") {
            Description = "Whether to escape unicode characters in JSON output. Default is true.",
            DefaultValueFactory = _ => true
        };

    /// <summary>
    /// バックアップするレコードをフィルタリングするためのクエリ式を指定するオプション。例: "Status = 'Completed'". このオプションを使用すると、指定した条件に一致するレコードのみがバックアップされるようになる。デフォルトはnull（すべてのレコードをバックアップ）。
    /// </summary>
    public static readonly Option<string?> Query =
        new("--query") {
            Description = "Query expression to filter records for backup."
        };

    /// <summary>
    /// バックアップファイルを分割して保存する際のレコード数を指定するオプション。デフォルトはCursorFetchLimit（1000）。このオプションを使用すると、指定したレコード数ごとにバックアップファイルが分割されるようになる。例えば、Splitを1000に設定すると、1000レコードごとに新しいバックアップファイルが作成される。最大値は5000で、それ以上の値を指定すると例外がスローされる。
    /// </summary>
    public static readonly Option<int> Split =
        new("--split") {
            Description = $"Number of records per split file. If not specified, all records are saved in a single file. Default is {CursorFetchLimit}. Maximum is 5000.",
            DefaultValueFactory = _ => CursorFetchLimit
        };
    #endregion

    #region <<for RestoreCommand>>
    /// <summary>
    /// 復元するバックアップのルートディレクトリを指定するオプション。必須。例: "backup/AppID000001/20260101_123456"。このディレクトリの下に、復元に必要なファイル（records.json、field_schema.json、files/など）が存在している必要がある。
    /// </summary>
    public static readonly Option<string?> BackupDir =
        new("--backup-dir") {
            Description = "Root directory of the backup (e.g., backup/AppID000001/20260101_123456)"
        };

    /// <summary>
    /// 復元モードを指定するオプション。FullReplace（完全置換）、Upsert（更新/挿入）、Merge（マージ）から選択可能。デフォルトはUpsert。FullReplaceは、復元先のアプリの既存レコードをすべて削除してからバックアップのレコードを挿入するモード。Upsertは、バックアップのレコードIDが復元先のアプリに存在する場合は更新し、存在しない場合は新規に挿入するモード。Mergeは、バックアップのレコードIDが復元先のアプリに存在する場合はフィールドごとに値をマージし、存在しない場合は新規に挿入するモード。
    /// </summary>
    public static readonly Option<RestoreMode> Mode =
        new("--mode") {
            Description = "Restore mode (FullReplace, Upsert, Merge). Default is Upsert",
            DefaultValueFactory = _ => RestoreMode.Upsert
        };

    /// <summary>
    /// スキーマ検証をスキップして危険な操作を許可するかどうかを指定するオプション。デフォルトはfalse。trueに設定すると、復元時にフィールドの型や設定の不一致があっても復元処理が続行されるようになる。ただし、これによりデータの整合性が損なわれる可能性があるため、使用には注意が必要。
    /// </summary>
    public static readonly Option<bool> Force =
        new("--force") {
            Description = "Skip schema validation and allow dangerous operations. Use with caution.",
            DefaultValueFactory = _ => false
        };

    /// <summary>
    /// 復元処理中にエラーが発生した場合の動作を指定するオプション。Continue（続行）、Abort（中止）から選択可能。デフォルトはContinue。Continueは、エラーが発生しても復元処理を続行するモード。Abortは、エラーが発生した時点で復元処理を中止するモード。
    /// </summary>
    public static readonly Option<bool> RestoreFiles =
        new("--restore-files") {
            Description = "Restore attached files (if any)",
            DefaultValueFactory = _ => true
        };

    /// <summary>
    /// 復元処理を実際に実行せず、どのような変更が行われるかをシミュレーションするオプション。デフォルトはfalse。trueに設定すると、復元処理が実行される代わりに、どのレコードが更新/挿入されるか、どのファイルが復元されるかなどの情報が出力されるようになる。これにより、復元処理を実行する前に、どのような変更が行われるかを確認することができる。
    /// </summary>
    public static readonly Option<bool> DryRun =
        new("--dry-run") {
            Description = "Perform a dry run without making actual changes",
            DefaultValueFactory = _ => false
        };

    /// <summary>
    /// 復元リクエストを実行せずに、リクエストの内容を検証するオプション。デフォルトはfalse。trueに設定すると、復元処理が実行される前に、リクエストの内容が正しいかどうかが検証されるようになる。これにより、復元処理を実行する前に、リクエストの内容に問題がないかを確認することができる。
    /// </summary>
    public static readonly Option<bool> ValidateOnly =
        new("--validate-only") {
            Description = "Only validate the restore request without executing",
            DefaultValueFactory = _ => false
        };

    /// <summary>
    /// バッチ処理で一度に処理するレコード数を指定するオプション。デフォルトはKintoneConstants.KintoneLimit（100件）。このオプションを使用すると、指定したレコード数ごとにバッチ処理が行われるようになる。
    /// </summary>
    public static readonly Option<int> BatchSize =
        new("--batch-size") {
            Description = "Number of records to process in each batch",
            DefaultValueFactory = _ => KintoneLimit
        };
    #endregion
}
