# kbackup

## 1. Overview（概要）

kbackup は、Kintone のレコードとフィールド情報をバックアップするための軽量 CLI ツールです。
バックアップ処理の本体は `KintoneNetLibrary.Backup` が担当しており、kbackup はその薄い CLI ラッパーとして動作します。

## 2. Requirements（動作環境）

- .NET 10
- Kintone API Token
- Windows / macOS / Linux で動作します

## 3. Installation（インストール）

1. リポジトリをクローンします。
   ```bash
   git clone https://github.com/k14a/kbackup.git
   cd kbackup
   ```
2. ビルドします。
   ```bash
   dotnet build
   ```
3. 実行します。
   ```bash
   dotnet run --project src/KBackup.Cli/KBackup.Cli.csproj -- backup --app-id <APP_ID> --api-token <API_TOKEN>
   ```

必要に応じて、ローカルツールとして `dotnet tool` から実行することも可能です。

## 4. Usage（最小の使用例）

```bash
dotnet run --project src/KBackup.Cli/KBackup.Cli.csproj -- backup --app-id 123 --api-token YOUR_API_TOKEN
```

この例では、必要最低限のオプションのみを指定してアプリのバックアップを実行します。

## 5. Output（生成されるファイル）

バックアップ実行後、次のファイルとディレクトリが生成されます。

- `fields.json`
- `records/` ディレクトリ（500件ごとに分割されたレコードファイル）

出力ディレクトリの構造例:

```
output/
  fields.json
  records/
    records-00001.json
    records-00002.json
    ...
```

## 6. How It Works（内部構造の簡単な説明）

kbackup のバックアップ処理本体は `KintoneNetLibrary.Backup` に委譲されています。
このリポジトリでは、CLI 入力の解釈と出力ファイルの生成を行う薄いラッパーを提供します。

## 7. Relation to kmodel（kmodel との連携）

`fields.json` は kmodel のモデル生成に利用できます。
kmodel でのモデル定義や型生成の元データとして活用可能です。

## 8. License（MIT）

MIT License
