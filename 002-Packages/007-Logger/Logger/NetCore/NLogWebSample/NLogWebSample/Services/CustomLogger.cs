using NLog;
using NLog.Config;

namespace NLogWebSample.Services
{
    public class CustomLogger
    {
        //private readonly ILogger _logger;
        private readonly LogFactory _logFactory;

        public CustomLogger()
        {
            _logFactory = new LogFactory(new XmlLoggingConfiguration("NLog-Part.config"));
        }

        public void Info(string id, string message)
        {
            var logger = _logFactory.GetCurrentClassLogger().WithProperty("EventId_Name", id);
            logger.Info(message);
        }
    }
}
