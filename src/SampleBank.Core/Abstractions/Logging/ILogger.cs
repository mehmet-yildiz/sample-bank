using System;

namespace SampleBank.Core.Abstractions.Logging
{
    public interface ILogger
    {
        void Log<TState>(LogLevel logLevel, TState state, Exception exception, Func<TState, Exception, string> formatter);

        void LogError(string msg);
        void LogError(Exception ex);
        void LogWarning(string msg);
        void LogInformation(string msg);
    }

    public enum LogLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical,
        None,
    }
}
