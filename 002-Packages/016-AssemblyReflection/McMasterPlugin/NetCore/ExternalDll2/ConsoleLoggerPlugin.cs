using MyPlugin;
using Newtonsoft.Json.Linq;
using System;

namespace ExternalDll2
{
    public class ConsoleLoggerPlugin : AbstructLoggerPlugin
    {
        public ConsoleLoggerPlugin(string level) : base(level)
        {
        }

        public override object Execute<T>(T param)
        {
            return CreateLogMessage(param.ToString());
        }

        private object CreateLogMessage(string message)
        {
            string logMessage = string.Empty;

            if (message.Length == 0)
            {
                logMessage = $"[{DateTime.Now.ToString("HH:mm:ss")}][{Level}]";
            }
            else
            {
                logMessage = $"[{DateTime.Now.ToString("HH:mm:ss")}][{Level}] {message}";
            }

            Console.WriteLine(logMessage);
            Console.WriteLine(typeof(JToken).Assembly.GetName().ToString());

            return logMessage;
        }
    }
}
