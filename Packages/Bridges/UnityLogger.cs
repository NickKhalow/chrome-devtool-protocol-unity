#nullable enable

using System;
using Microsoft.Extensions.Logging;
using UnityEngine;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace CDPBridges
{
    public class UnityLogger : ILogger
    {
        private readonly string categoryName;

        public UnityLogger(string categoryName)
        {
            this.categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            // Scopes are used for structured logging; not needed in Unity
            return NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            string message = formatter(state, exception);

            if (!string.IsNullOrEmpty(message))
            {
                message = $"[{categoryName}] {message}";
            }

            if (exception != null)
            {
                message += $"\nException: {exception}";
            }

            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Information:
                    Debug.Log(message);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogLevel.Error:
                case LogLevel.Critical:
                    Debug.LogError(message);
                    break;
                case LogLevel.None:
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }


        private class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new();

            public void Dispose()
            {
            }
        }
    }
}