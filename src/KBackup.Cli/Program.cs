using System.CommandLine;
using System.ComponentModel;
using KBackup.Cli.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

// -----------------------------
// DI: Logging
// -----------------------------
builder.Services.AddLogging(config => {
    config.AddConsole();
});

// -----------------------------
// DI: KintoneNetLibrary 系
// -----------------------------
builder.Services.AddSingleton<IBackupService, BackupService>();
builder.Services.AddSingleton<IKintoneBackupService, KintoneBackupService>();
builder.Services.AddSingleton<IKintoneCodeGenService, KintoneCodeGenService>();

// -----------------------------
// DI: kbackup Core / Infrastructure
// -----------------------------
builder.Services.AddSingleton<IBackupUseCase, BackupUseCase>();
builder.Services.AddSingleton<IKintoneRepository, KintoneRepository>();

var host = builder.Build();

// -----------------------------
// CLI 定義
// -----------------------------
var root = new RootCommand("kbackup CLI tool") {
    new BackupCommand(host)
};


// -----------------------------
// 実行（最小限）
// -----------------------------
var logger = host.Services.GetRequiredService<ILoggerFactory>()
                          .CreateLogger("kbackup");

logger.LogInformation("kbackup CLI started.");

try {
    // ここは後で System.CommandLine に置き換える
    logger.LogInformation("No command implemented yet.");

} catch (Exception ex) {
    logger.LogError(ex, "Unhandled exception occurred.");
}

logger.LogInformation("kbackup CLI finished.");
