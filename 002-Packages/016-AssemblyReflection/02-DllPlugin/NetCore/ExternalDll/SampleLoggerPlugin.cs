using MyPlugin;
using Newtonsoft.Json.Linq;
using System;

namespace ExternalDll
{
    public class SampleLoggerPlugin : AbstructLoggerPlugin
    {
        public SampleLoggerPlugin(string level) : base(level)
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
                logMessage = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][{Level}]";
            }
            else
            {
                logMessage = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][{Level}] {message}";
            }

            Console.WriteLine(logMessage);
            Console.WriteLine(typeof(JToken).Assembly.GetName().ToString());

            return logMessage;
        }
    }
}
