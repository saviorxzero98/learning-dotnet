using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogSample
{
    class Program
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("InfoMessage");
            logger.Debug("DebugMessage");
            logger.Warn("WarnMessage");
            try
            {
                expection();
            }
            catch (Exception e) {
                logger.Error(e);
                logger.Fatal(e);
            }

            // Custom Name Log File
            var customLogger = LogManager.GetLogger("custom");
            LogManager.Configuration.Variables["MyName"] = "AA";
            customLogger.Info("AA");

            LogManager.Configuration.Variables["MyName"] = "BB";
            customLogger.Info("BB");

            LogManager.Configuration.Variables["MyName"] = "CC";
            customLogger.Info("CC");
        }

        static void expection() {
            logger.Trace("Trace");
            throw new Exception("Expect Event");
        }
    }
}
