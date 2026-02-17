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
    /// MethodInfo メソッドは、指定された ILogger を使用して、クラス名とメソッド名を含む情報レベルのログメッセージを出力します。呼び出し元の情報は、CallerMemberName、CallerFilePath、および CallerLineNumber 属性を使用して自動的に取得されます。
    /// </summary>
    /// <param name="logger">ログを出力する ILogger オブジェクト。</param>
    /// <param name="ex">例外オブジェクト（任意）。</param>
    /// <param name="message">ログメッセージ（任意）。</param>
    /// <param name="methodName">呼び出し元のメソッド名（自動取得）。</param>
    /// <param name="filePath">呼び出し元のファイルパス（自動取得）。</param>
    /// <param name="lineNumber">呼び出し元の行番号（自動取得）。</param>
    public static void MethodInfo(ILogger? logger, Exception? ex = null, string message = "", [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
        if (logger == null) { return; }
        var className = Path.GetFileNameWithoutExtension(filePath);
        methodName = lineNumber > 0 ? $"{methodName}[{lineNumber}]" : methodName;
        MethodInfoInner(logger, className, methodName, message, ex);
    }

    /// <summary>
    /// MethodWarning メソッドは、指定された ILogger を使用して、クラス名とメソッド名を含む警告レベルのログメッセージを出力します。呼び出し元の情報は、CallerMemberName、CallerFilePath、および CallerLineNumber 属性を使用して自動的に取得されます。
    /// </summary>
    /// <param name="logger">ログを出力する ILogger オブジェクト。</param>
    /// <param name="ex">例外オブジェクト（任意）。</param>
    /// <param name="message">ログメッセージ（任意）。</param>
    /// <param name="methodName">呼び出し元のメソッド名（自動取得）。</param>
    /// <param name="filePath">呼び出し元のファイルパス（自動取得）。</param>
    /// <param name="lineNumber">呼び出し元の行番号（自動取得）。</param>
    public static void MethodWarning(ILogger? logger, Exception? ex = null, string message = "", [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
        if (logger == null) { return; }
        var className = Path.GetFileNameWithoutExtension(filePath);
        methodName = lineNumber > 0 ? $"{methodName}[{lineNumber}]" : methodName;
        MethodWarningInner(logger, className, methodName, message, ex);
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

    /// <summary>
    /// MethodDebug メソッドは、指定された ILogger を使用して、クラス名とメソッド名を含むデバッグレベルのログメッセージを出力します。呼び出し元の情報は、CallerMemberName、CallerFilePath、および CallerLineNumber 属性を使用して自動的に取得されます。
    /// </summary>
    /// <param name="logger">ログを出力する ILogger オブジェクト。</param>
    /// <param name="ex">例外オブジェクト（任意）。</param>
    /// <param name="message">ログメッセージ（任意）。</param>
    /// <param name="methodName">呼び出し元のメソッド名（自動取得）。</param>
    /// <param name="filePath">呼び出し元のファイルパス（自動取得）。</param>
    /// <param name="lineNumber">呼び出し元の行番号（自動取得）。</param>
    public static void MethodDebug(ILogger? logger, Exception? ex = null, string message = "", [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) {
        if (logger == null) { return; }
        var className = Path.GetFileNameWithoutExtension(filePath);
        methodName = lineNumber > 0 ? $"{methodName}[{lineNumber}]" : methodName;
        MethodDebugInner(logger, className, methodName, message, ex);
    }
    #endregion

    #region <<Private methods>>
    /// <summary>
    /// MethodStartInner、MethodFinishInner、MethodInfoInner、MethodWarningInner、MethodErrorInner、および MethodDebugInner は、LoggerMessage.Define メソッドを使用して定義された静的な Action デリゲートであり、指定された ILogger を使用してログメッセージを出力します。これらのデリゲートは、クラス名とメソッド名を含むログメッセージのテンプレートを定義し、ログレベルとイベントIDを指定しています。
    /// </summary>
    private static readonly Action<ILogger, string, string, Exception?> MethodStartInner =
        LoggerMessage.Define<string, string>(
            LogLevel.Debug,
            new EventId(1001, nameof(MethodStart)),
            "{Class}.{Method} Start.");

    /// <summary>
    /// MethodStartInner、MethodFinishInner、MethodInfoInner、MethodWarningInner、MethodErrorInner、および MethodDebugInner は、LoggerMessage.Define メソッドを使用して定義された静的な Action デリゲートであり、指定された ILogger を使用してログメッセージを出力します。これらのデリゲートは、クラス名とメソッド名を含むログメッセージのテンプレートを定義し、ログレベルとイベントIDを指定しています。
    /// </summary>
    private static readonly Action<ILogger, string, string, Exception?> MethodFinishInner =
        LoggerMessage.Define<string, string>(
            LogLevel.Debug,
            new EventId(1002, nameof(MethodFinish)),
            "{Class}.{Method} Finish.");

    /// <summary>
    /// MethodStartInner、MethodFinishInner、MethodInfoInner、MethodWarningInner、MethodErrorInner、および MethodDebugInner は、LoggerMessage.Define メソッドを使用して定義された静的な Action デリゲートであり、指定された ILogger を使用してログメッセージを出力します。これらのデリゲートは、クラス名とメソッド名を含むログメッセージのテンプレートを定義し、ログレベルとイベントIDを指定しています。
    /// </summary>
    private static readonly Action<ILogger, string, string, string, Exception?> MethodInfoInner =
        LoggerMessage.Define<string, string, string>(
            LogLevel.Information,
            new EventId(1003, nameof(MethodInfo)),
            "{Class}.{Method} {Message}.");

    /// <summary>
    /// MethodStartInner、MethodFinishInner、MethodInfoInner、MethodWarningInner、MethodErrorInner、および MethodDebugInner は、LoggerMessage.Define メソッドを使用して定義された静的な Action デリゲートであり、指定された ILogger を使用してログメッセージを出力します。これらのデリゲートは、クラス名とメソッド名を含むログメッセージのテンプレートを定義し、ログレベルとイベントIDを指定しています。
    /// </summary>
    private static readonly Action<ILogger, string, string, string, Exception?> MethodWarningInner =
        LoggerMessage.Define<string, string, string>(
            LogLevel.Warning,
            new EventId(1004, nameof(MethodWarning)),
            "{Class}.{Method} {Message}.");

    /// <summary>
    /// MethodStartInner、MethodFinishInner、MethodInfoInner、MethodWarningInner、MethodErrorInner、および MethodDebugInner は、LoggerMessage.Define メソッドを使用して定義された静的な Action デリゲートであり、指定された ILogger を使用してログメッセージを出力します。これらのデリゲートは、クラス名とメソッド名を含むログメッセージのテンプレートを定義し、ログレベルとイベントIDを指定しています。
    /// </summary>
    private static readonly Action<ILogger, string, string, string, Exception?> MethodErrorInner =
        LoggerMessage.Define<string, string, string>(
            LogLevel.Error,
            new EventId(1005, nameof(MethodError)),
            "{Class}.{Method} {Message}.");

    /// <summary>
    /// MethodStartInner、MethodFinishInner、MethodInfoInner、MethodWarningInner、MethodErrorInner、および MethodDebugInner は、LoggerMessage.Define メソッドを使用して定義された静的な Action デリゲートであり、指定された ILogger を使用してログメッセージを出力します。これらのデリゲートは、クラス名とメソッド名を含むログメッセージのテンプレートを定義し、ログレベルとイベントIDを指定しています。
    /// </summary>
    private static readonly Action<ILogger, string, string, string, Exception?> MethodDebugInner =
        LoggerMessage.Define<string, string, string>(
            LogLevel.Debug,
            new EventId(1006, nameof(MethodDebug)),
            "{Class}.{Method} {Message}.");
    #endregion
}
