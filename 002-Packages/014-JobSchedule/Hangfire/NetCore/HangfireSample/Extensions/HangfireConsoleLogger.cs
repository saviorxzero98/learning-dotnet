using Hangfire.Console;
using Hangfire.Server;
using Microsoft.Extensions.Logging;
using System;

namespace HangfireSample.Extensions
{
    public class HangfireConsoleLogger : ILogger
    {
        private readonly PerformContext _performContext;

        public HangfireConsoleLogger(PerformContext performContext)
        {
            _performContext = performContext;
        }


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
                                Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            ConsoleTextColor color = ConsoleTextColor.White;
            switch (logLevel)
            {
                case LogLevel.Trace:
                    color = ConsoleTextColor.Gray;
                    break;

                case LogLevel.Debug:
                    color = ConsoleTextColor.Magenta;
                    break;

                case LogLevel.Information:
                    color = ConsoleTextColor.Green;
                    break;

                case LogLevel.Warning:
                    color = ConsoleTextColor.Yellow;
                    break;

                case LogLevel.Error:
                case LogLevel.Critical:
                    color = ConsoleTextColor.Red;
                    break;
            }

            _performContext.WriteLine(color, formatter(state, exception));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (logLevel != LogLevel.None);
        }
    }
}
