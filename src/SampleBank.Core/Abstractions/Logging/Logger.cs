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
            throw new NotImplementedException();
        }

        public void LogError(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string msg)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
