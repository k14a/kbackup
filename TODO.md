# コードレビュー TODO

## 凡例
- `[ ]` 未対応
- `[x]` 対応済み

---

## 推奨対応順序

| Phase | 対象ID | 理由 |
|---|---|---|
| **Phase 1** ビルドを通す | KB-01, KB-02, KB-03, KB-05 | KintoneNetLibrary の破壊的変更への追従。現状ビルドエラーの可能性あり。先決。 |
| **Phase 2** バグ修正 | NM-04, NM-01, NM-02, DS-04 | 動作に直接影響する問題（設定ファイルの誤参照、CLIオプション二重登録、出力メッセージ誤り）。 |
| **Phase 3** 安全なクリーンアップ | DU-01〜05, NM-05, NM-06, KB-04 | 変更範囲が小さく影響が限定的。未使用コード削除・名前統一。 |
| **Phase 4** 設計改善 | DS-03, DS-06, DS-01, DS-07, DS-05 | 品質向上。動作には影響しないが保守性に影響する。 |
| **Phase 5** アーキテクチャ改善 | CA-01, CA-02, CA-03 | Core 層の外部ライブラリ依存解消。影響範囲が最大のため最後に対応する。 |

---

## 1. Clean Architecture の依存方向

### Core 層が外部ライブラリの型に直接依存している

- [x] **[CA-01]** `KBackup.Core.csproj` が `KintoneNetLibrary.Backup` と `KintoneNetLibrary` をプロジェクト参照している
  - Domain/Application 層が Infrastructure 相当の外部ライブラリに依存しており、依存方向が逆になっている
  - `IBackupKintoneRepository`, `IBackupUseCase`, `IRestoreUseCase` のインターフェース定義が `KintoneNetLibrary.Backup.Application.DTOs.BackupResult/RestoreResult` を使用しており、Core が外部型に結合している
  - 対応方針: Core に独自の `BackupResult` / `RestoreResult` を定義し、Infrastructure 層でマッピングする

- [x] **[CA-02]** `RestoreRequest.cs` が `KintoneNetLibrary.Backup.Domain.Enums.RestoreMode` と `KintoneNetLibrary.Domain.Common` を直接参照している
  - Domain モデルが外部ライブラリの列挙型に依存している
  - 対応方針: Core に `RestoreMode` 等の列挙型を複製または抽象化し、Infrastructure 層で変換する

### Cli 層が Infrastructure 内部型を参照している

- [x] **[CA-03]** `BackupCommand.cs` が `KintoneNetLibrary` 系の型(`IKintoneAccessFactory` 等)を import している（コメントアウトコードも含む）
  - Cli は Core の抽象に依存すべきで、外部ライブラリ型への依存は最小限にすべき

---

## 2. 命名規則の一貫性

- [x] **[NM-01]** `BaseCommand.ColorOption`（CLIオプション名 `--color`）と `OptionFactory.OutputColorMode`（CLIオプション名 `--color-mode`）が並存している
  - 両方がコマンドに追加されており、`--color` と `--color-mode` という類似したオプションが重複定義されている
  - 実際に使われるのは `OptionFactory.OutputColorMode` のみで、`BaseCommand.ColorOption` は機能していない

- [x] **[NM-02]** `BaseCommand.JsonOption`（CLIオプション名 `--json`）と `OptionFactory.JsonOutput`（CLIオプション名 `--json`）が同じ名前で重複している
  - `BaseCommand` コンストラクタで `--json` を追加し、子クラスでも `OptionFactory.JsonOutput`（`--json`）を追加しているため、同名オプションが二重登録される

- [x] **[NM-03]** CLI オプション名とプロパティ名の不一致
  - `OptionFactory.Split`（`--split`）→ `BackupOptions.SplitSize` / `BackupRequest.SplitSize`（`SplitSize`）
  - `OptionFactory.OutputColorMode`（`--color-mode`）→ プロパティ名は `ColorMode`

- [x] **[NM-04]** `BackupOptionsResolver` で `config?.BackupDirectory` を `BackupOptions.OutputDir` にマッピングしている
  - `KBackupConfig.OutputDirectory` を使うべきところを `config?.BackupDirectory`（リストア用プロパティ）に誤って参照している

- [x] **[NM-05]** `KintoneRepository.Backup.cs` / `KintoneRepository.Restore.cs` のファイル名がクラス名 `BackupKintoneRepository` と一致していない
  - 正しくは `BackupKintoneRepository.Backup.cs` / `BackupKintoneRepository.Restore.cs` とすべき

- [x] **[NM-06]** `BackupOptions.OutputDir` と `KBackupConfig.OutputDirectory` で同じ概念に異なる名前を使用している

---

## 3. 設計上の問題

- [x] **[DS-01]** `IBackupKintoneRepository` がバックアップとリストアの両方を担っており、Single Responsibility Principle に違反している
  - インターフェース名が `IBackupKintoneRepository` なのにリストア操作も含んでいる
  - 対応方針: `IBackupKintoneRepository` と `IRestoreKintoneRepository` に分離するか、`IKintoneRepository` に改名して責務を明示する

- [x] **[DS-02]** `BackupService` / `RestoreService` が Singleton 登録されているが、`Options` プロパティへの代入がスレッドアンセーフ
  - KB-01/KB-02 で Options セッターをメソッド引数に変更、KB-05 で Transient 登録に変更済みのため解消

- [x] **[DS-03]** `RestoreUseCase` と `BackupUseCase` でロギングパターンが不一致
  - `BackupUseCase` は `StaticLogMessages.MethodStart/MethodFinish` を使用
  - `RestoreUseCase` は `this._logger.LogInformation` を直接使用しており、try-finally でのログ保証もない

- [x] **[DS-04]** `CommandUtils.PrintResult` のメッセージが "Backup" にハードコードされている
  - `RestoreCommand` でも同じメソッドを使用しているが、"Backup completed successfully." と表示される
  - 対応方針: `result` の種別を判断するか、メッセージを引数化する

- [x] **[DS-05]** `KBackupConfig` が BackupとRestoreの設定を1クラスに混在させている
  - `Force` は `RestoreRequest` にのみ存在するが、Backupコマンド実行時も `KBackupConfig.Force` が存在する
  - 対応方針: `BackupConfig` と `RestoreConfig` に分割するか、セクションを明確に区切る

- [x] **[DS-06]** `BaseCommand.Resolve<T>` 静的メソッドが `BaseCommand` クラスに定義されている
  - `BackupOptionsResolver` / `RestoreOptionsResolver` から `BaseCommand.Resolve(...)` として呼ばれているが、命名と責務が合っていない
  - 対応方針: `OptionResolver` などの独立したクラスに移動するか、`BackupOptionsResolver` / `RestoreOptionsResolver` 内に移動する

- [x] **[DS-07]** `BackupKintoneRepository` のコンストラクタが `IRestoreService` を受け取っているが、クラス名が `BackupKintoneRepository`
  - [DS-01] と関連：クラス名がバックアップ専用を示唆しているのに Restore も実装している

---

## 4. 未使用コード・重複コード

- [x] **[DU-01]** `KintoneBackupData` クラスと `KintoneFileData` クラス（`KBackup.Core/KintoneBackupData.cs`）が未使用
  - どのクラスからも参照されていない
  - 外部ライブラリの型で結果を返しているため、独自クラスが不要になっている可能性がある

- [x] **[DU-02]** `PathHelper.BuildOutputPath`（`KBackup.Cli/Utils/PathHelper.cs`）が未使用
  - `BackupRequestFactory` では `new DirectoryInfo(opts.OutputDir)` を直接使用している
  - `PathHelper` 自体が孤立したユーティリティになっている

- [x] **[DU-03]** `BaseCommand.ColorOption` と `BaseCommand.JsonOption` が実質未使用
  - `BackupCommand` / `RestoreCommand` は `OptionFactory.OutputColorMode` と `OptionFactory.JsonOutput` を使用しており、`BaseCommand` の同名フィールドは参照されていない
  - かつ `BaseCommand` コンストラクタで登録済みの `--color` / `--json` と、子クラスで追加する `OptionFactory` の `--color-mode` / `--json` が共存しているため、オプション定義が混乱している

- [x] **[DU-04]** `StaticLogMessages` の多くのメソッドが未使用
  - `MethodInfo`, `MethodWarning`, `MethodError`, `MethodDebug` は定義されているが実際には呼び出されていない
  - 使用するかどうかを決定し、不要なら削除する

- [x] **[DU-05]** コメントアウトされたコードが残っている
  - `BackupCommand.cs:50-51` — `IKintoneAccessFactory` / `kintoneAccess` のコメントアウト
  - `RestoreRequestFactory.cs:25-26` — `ManifestLoader.Load` のコメントアウト
  - `RestoreRequestFactory.cs:66-67` — `Force` の追加条件チェックのコメントアウト

---

## 5. KintoneNetLibrary リファクタリングへの追従

> KintoneNetLibrary 側のリファクタリングに合わせて kbackup 側を更新する必要がある。
> 参照先: `~/source/KintoneNetLibrary`

### インターフェースシグネチャの変更（破壊的変更）

- [x] **[KB-01]** `IBackupService` のシグネチャ変更への対応
  - 旧: `Options` プロパティ(setter) + `RunBackupAsync()` (パラメータなし)
  - 新: `RunBackupAsync(BackupOptions options)` (パラメータあり、`Options` セッター廃止)
  - 対象: `KintoneRepository.Backup.cs` の以下のコードを修正する
    ```csharp
    // 現在（旧シグネチャ）
    this._backupService.Options = new BackupOptions { ... };
    return await this._backupService.RunBackupAsync();
    // 変更後（新シグネチャ）
    var options = new BackupOptions { ... };
    return await this._backupService.RunBackupAsync(options);
    ```
  - この変更により [DS-02] のスレッドアンセーフ問題も自然に解消される

- [x] **[KB-02]** `IRestoreService` のシグネチャ変更への対応
  - 旧: `Options` プロパティ(setter) + `RunRestoreAsync()` (パラメータなし)
  - 新: `RunRestoreAsync(RestoreOptions options)` (パラメータあり)
  - 対象: `KintoneRepository.Restore.cs` の以下のコードを修正する
    ```csharp
    // 現在（旧シグネチャ）
    this._restoreService.Options = options;
    return await this._restoreService.RunRestoreAsync();
    // 変更後（新シグネチャ）
    return await this._restoreService.RunRestoreAsync(options);
    ```

### DI 登録の追加

- [x] **[KB-03]** `Program.cs` に `IKintoneApiFactory` の DI 登録を追加する
  - `BackupService` / `RestoreService` のコンストラクタに `IKintoneApiFactory` が追加された
  - kbackup の `Program.cs` には現在 `IKintoneApiFactory` が登録されていない
  - 追加すべき登録:
    ```csharp
    builder.Services.AddTransient<IKintoneApiFactory, KintoneApiFactory>();
    ```

### `BackupOptions` の新プロパティへの対応

- [x] **[KB-04]** `BackupOptions`（KintoneNetLibrary 側）に追加された新プロパティを `BackupRequest` に反映する
  - 新プロパティ: `FieldCodes`（`IList<string>?`）― 取得フィールドコードを絞り込む
  - 新プロパティ: `BatchSize`（`int?`）― カーソルページサイズ（バックアップ時のページ単位レコード数）
  - 新プロパティ: `FieldSchemaFileName`（`string`、デフォルト `"fields.json"`）― スキーマファイル名
  - 対応方針: 必要なら `BackupRequest` / `BackupOptions`（kbackup 側）/ `BackupOptionsResolver` / CLI オプションにも追加する
  - 少なくとも `KintoneRepository.Backup.cs` のマッピング箇所で `null` を渡しているプロパティを明示的にコメントする

### DI ライフタイムの見直し

- [x] **[KB-05]** `BackupService` / `RestoreService` の DI 登録を `Singleton` から `Transient` に変更する
  - 新しいインターフェースではオプションはメソッド引数で渡されるため Singleton でも動作するが、
    実装内部（`BackupService.cs`）で `_options`・`_api`・`_partIndex` などのインスタンスフィールドに状態を持つ設計のため、並行実行時に競合リスクがある
  - `Transient` に変更することでリクエストごとに新インスタンスを生成し安全にする
  - 対象: `Program.cs` の `AddSingleton<IBackupService>` / `AddSingleton<IRestoreService>` を `AddTransient` に変更
