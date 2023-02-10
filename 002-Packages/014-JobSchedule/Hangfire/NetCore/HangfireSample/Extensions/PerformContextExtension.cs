using Hangfire.Console;
using Hangfire.Server;

namespace HangfireSample.Extensions
{
    public static class PerformContextExtension
    {
        public static void LogTrace(this PerformContext context, string message, params object[] args)
        {
            context.WriteLine(ConsoleTextColor.DarkGray, message, args);
        }

        public static void LogDebug(this PerformContext context, string message, params object[] args)
        {
            context.WriteLine(ConsoleTextColor.Magenta, message, args);
        }

        public static void LogInformation(this PerformContext context, string message, params object[] args)
        {
            context.WriteLine(ConsoleTextColor.Green, message, args);
        }

        public static void LogWarning(this PerformContext context, string message, params object[] args)
        {
            context.WriteLine(ConsoleTextColor.Yellow, message, args);
        }

        public static void LogError(this PerformContext context, string message, params object[] args)
        {
            context.WriteLine(ConsoleTextColor.Red, message, args);
        }

        public static void LogCritical(this PerformContext context, string message, params object[] args)
        {
            context.WriteLine(ConsoleTextColor.Red, message, args);
        }

        public static void Log(this PerformContext context, string message, params object[] args)
        {
            context.WriteLine(message, args);
        }
    }
}
