using MyPlugin;
using Newtonsoft.Json.Linq;
using System;

namespace ExternalDll
{
    public class ObjectLoggerPlugin : AbstructLoggerPlugin
    {
        public ObjectLoggerPlugin(string level) : base(level)
        {
        }

        public override object Execute<T>(T param)
        {
            return CreateLogMessage(JToken.FromObject(param));
        }

        private object CreateLogMessage<T>(T param)
        {
            string logMessage = string.Empty;

            if (param == null)
            {
                logMessage = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][{Level}]";
            }
            else
            {
                var json = JToken.FromObject(param).ToString();
                logMessage = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][{Level}] {json}";
            }

            Console.WriteLine(logMessage);
            Console.WriteLine(typeof(JToken).Assembly.GetName().ToString());

            return logMessage;
        }
    }
}
