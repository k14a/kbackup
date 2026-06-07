using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace KBackup.Core.Logging;

/// <summary>
/// StaticLogMessages クラスは、ログメッセージの定義とロギングのための静的メソッドを提供するクラスです。MethodStart、MethodFinish、MethodInfo、MethodWarning、MethodError、MethodDebug の各メソッドは、指定された ILogger を使用して、クラス名とメソッド名を含むログメッセージを出力します。これらのメソッドは、呼び出し元の情報を自動的に取得するために CallerMemberName、CallerFilePath、および CallerLineNumber 属性を使用しています。
/// </summary>
public static class StaticLogMessages {
    #region <<Public methods>>
    /// <summary>
    /// MethodStart メソッドは、指定された ILogger を使用して、クラス名とメソッド名を含む "Start" ログメッセージを出力します。呼び出し元の情報は、CallerMemberName、CallerFilePath、および CallerLineNumber 属性を使用して自動的に取得されます。
    /// </summary>
    /// <param name="logger">ログを出力する ILogger オブジェクト。</param>
    /// <param name="ex">例外オブジェクト（任意）。</param>
    /// <param name="message">ログメッセージ（任意）。</param>
    /// <param name="methodName">呼び出し元のメソッド名（自動取得）。</param>
    /// <param name="filePath">呼び出し元のファイルパス（自動取得）。</param>
    /// <param name="lineNumber">呼び出し元の行番号（自動取得）。</param>
    public static void MethodStart(ILogger? logger, Exception? ex = null, string? message = null, [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
        if (logger == null) { return; }
        var className = Path.GetFileNameWithoutExtension(filePath);
        methodName = lineNumber > 0 ? $"{methodName}[{lineNumber}]" : methodName;
        MethodStartInner(logger, className, methodName, ex);
    }

    /// <summary>
    /// MethodFinish メソッドは、指定された ILogger を使用して、クラス名とメソッド名を含む "Finish" ログメッセージを出力します。呼び出し元の情報は、CallerMemberName、CallerFilePath、および CallerLineNumber 属性を使用して自動的に取得されます。
    /// </summary>
    /// <param name="logger">ログを出力する ILogger オブジェクト。</param>
    /// <param name="ex">例外オブジェクト（任意）。</param>
    /// <param name="message">ログメッセージ（任意）。</param>
    /// <param name="methodName">呼び出し元のメソッド名（自動取得）。</param>
    /// <param name="filePath">呼び出し元のファイルパス（自動取得）。</param>
    /// <param name="lineNumber">呼び出し元の行番号（自動取得）。</param>
    public static void MethodFinish(ILogger? logger, Exception? ex = null, string? message = null, [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
        if (logger == null) { return; }
        var className = Path.GetFileNameWithoutExtension(filePath);
        methodName = lineNumber > 0 ? $"{methodName}[{lineNumber}]" : methodName;
        MethodFinishInner(logger, className, methodName, ex);
    }

    /// <summary>
    /// MethodError メソッドは、指定された ILogger を使用して、クラス名とメソッド名を含むエラーレベルのログメッセージを出力します。呼び出し元の情報は、CallerMemberName、CallerFilePath、および CallerLineNumber 属性を使用して自動的に取得されます。
    /// </summary>
    /// <param name="logger">ログを出力する ILogger オブジェクト。</param>
    /// <param name="ex">例外オブジェクト（任意）。</param>
    /// <param name="message">ログメッセージ（任意）。</param>
    /// <param name="methodName">呼び出し元のメソッド名（自動取得）。</param>
    /// <param name="filePath">呼び出し元のファイルパス（自動取得）。</param>
    /// <param name="lineNumber">呼び出し元の行番号（自動取得）。</param>
    public static void MethodError(ILogger? logger, Exception? ex = null, string message = "", [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
        if (logger == null) { return; }
        var className = Path.GetFileNameWithoutExtension(filePath);
        methodName = lineNumber > 0 ? $"{methodName}[{lineNumber}]" : methodName;
        MethodErrorInner(logger, className, methodName, message, ex);
    }
    #endregion

    #region <<Private methods>>
    private static readonly Action<ILogger, string, string, Exception?> MethodStartInner =
        LoggerMessage.Define<string, string>(
            LogLevel.Debug,
            new EventId(1001, nameof(MethodStart)),
            "{Class}.{Method} Start.");

    private static readonly Action<ILogger, string, string, Exception?> MethodFinishInner =
        LoggerMessage.Define<string, string>(
            LogLevel.Debug,
            new EventId(1002, nameof(MethodFinish)),
            "{Class}.{Method} Finish.");

    private static readonly Action<ILogger, string, string, string, Exception?> MethodErrorInner =
        LoggerMessage.Define<string, string, string>(
            LogLevel.Error,
            new EventId(1005, nameof(MethodError)),
            "{Class}.{Method} {Message}.");
    #endregion
}
