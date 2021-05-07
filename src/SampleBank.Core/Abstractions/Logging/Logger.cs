using System;

namespace SampleBank.Core.Abstractions.Logging
{
    public class Logger : ILogger
    {
        public void Log<TState>(LogLevel logLevel, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }

        public void LogError(string msg)
        { 
        }

        public void LogError(Exception ex)
        {
        }

        public void LogWarning(string msg)
        {
        }

        public void LogInformation(string msg)
        {
        }
    }
}
