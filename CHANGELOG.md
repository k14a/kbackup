# Changelog

このプロジェクトのすべての注目すべき変更はこのファイルに記録されます。
形式は [Keep a Changelog](https://keepachangelog.com/ja/1.1.0/) に基づいています。

## [0.9.1] - 2026-06-08

### Added
- xUnit テストプロジェクトを追加 (`KBackup.Core.Tests` / `KBackup.Cli.Tests`)
  - `BackupUseCase` / `RestoreUseCase` の Moq ベースのユニットテスト
  - `BackupRequestFactory` / `RestoreRequestFactory` の境界値・例外テスト
  - `OptionResolver` の優先順位テスト（CLI > 設定ファイル > デフォルト値）
- GitHub Actions CI ワークフローを追加（push / PR 時に `dotnet test` を自動実行）
- Core 層に `IOperationResult` インターフェイス、`BackupResult`、`RestoreResult` を定義
- Core 層に `RestoreMode` 列挙型を定義
- `IRestoreKintoneRepository` インターフェイスを新設（バックアップとリストアのリポジトリを分離）
- `OptionResolver` クラスを新設（CLI・設定ファイル・デフォルト値の優先解決ロジックを集約）
- `BackupRequest` に `FieldCodes`・`BatchSize` プロパティを追加
- MIT License を追加

### Changed
- Core 層から KintoneNetLibrary への依存を除去（Infrastructure 層でマッピング）
- `IBackupService` / `IRestoreService` を Options セッター廃止・メソッド引数方式へ移行
- `BackupService` / `RestoreService` の DI スコープを Singleton → Transient へ変更
- `OptionFactory.Split` → `SplitSize`、`OptionFactory.OutputColorMode` → `ColorMode` にリネーム
- `BackupOptions.OutputDir` → `OutputDirectory` に統一
- `RestoreUseCase` のロギングを `BackupUseCase` と同一パターンに統一
- `KBackupConfig.Force` をリストアセクションへ移動
- `KintoneRepository` をバックアップ・リストアで部分クラスに分割

### Fixed
- `OptionResolver.Resolve` の CLI 明示指定判定を修正（`DefaultValueFactory` が設定されている場合でも正しく CLI 値を優先するよう `OptionResult.Implicit` で判定）
- `BaseCommand` の `ColorOption` / `JsonOption` 重複登録を修正
- `BackupOptionsResolver` の設定ファイルキー誤参照を修正（`BackupDirectory` → `OutputDirectory`）
- `CommandUtils.PrintResult` の操作名ハードコードを解消
- `KBackup.Cli.csproj` のプロジェクト参照パスを修正（MSB9008）
- `RestoreOptions.BackupDir` への null 代入に対する警告を解消（CS8601）

### Removed
- 未使用の `KintoneBackupData` クラスを削除
- 未使用の `PathHelper` クラスを削除
- `StaticLogMessages` の未使用メソッドを削除
- `BaseCommand` の未使用フィールドを削除
