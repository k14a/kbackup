using System.CommandLine;
using KBackup.Cli.Commands;
using KBackup.Core;
using KBackup.Core.Interfaces;
using KBackup.Infrastructure;
using KintoneNetLibrary.Application.Interfaces;
using KintoneNetLibrary.Backup.Application.Interfaces;
using KintoneNetLibrary.Backup.Infrastructure.Services;
using KintoneNetLibrary.CodeGen.Application.Interfaces;
using KintoneNetLibrary.CodeGen.Infrastructure.Services;
using KintoneNetLibrary.Infrastructure.Api;
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
builder.Services.AddSingleton<IRestoreService, RestoreService>();
builder.Services.AddHttpClient();
builder.Services.AddTransient<ISchemaProvider, SchemaProvider>();
builder.Services.AddTransient<IKintoneAppMetadataApi, KintoneAppMetadataApi>();

// -----------------------------
// DI: kbackup Core / Infrastructure
// -----------------------------
builder.Services.AddSingleton<IBackupUseCase, BackupUseCase>();
builder.Services.AddSingleton<IRestoreUseCase, RestoreUseCase>();
builder.Services.AddSingleton<IKintoneRepository, KintoneRepository>();

var host = builder.Build();

// -----------------------------
// CLI 定義
// -----------------------------
var backupLogger = host.Services.GetRequiredService<ILogger<BackupCommand>>();
var restoreLogger = host.Services.GetRequiredService<ILogger<RestoreCommand>>();
var root = new RootCommand("kbackup CLI tool") {
    new BackupCommand(host, backupLogger),
    new RestoreCommand(host, restoreLogger)
};


// -----------------------------
// 実行（最小限）
// -----------------------------
var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("kbackup");

// logger.LogInformation("kbackup CLI started.");

try {
    await root.Parse(args).InvokeAsync();

} catch (Exception ex) {
    logger.LogError(ex, "Unhandled exception occurred.");
}

// logger.LogInformation("kbackup CLI finished.");
