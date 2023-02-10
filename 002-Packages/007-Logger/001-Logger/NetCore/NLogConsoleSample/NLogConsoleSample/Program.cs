using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System;

namespace NLogConsoleSample
{
    class Program
    {
        static IConfiguration Configuration;


        static void Main(string[] args)
        {
            //var logger = LogManager.GetCurrentClassLogger();

            ILoggerFactory loggerFactory = new NLogLoggerFactory();
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("Hello Global");

            var nLogger = GetLogger("Part");
            nLogger.Info("Hello Part");

            //Startup();

            Console.WriteLine("Hello World!");
        }


        static void Startup()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                      .Build();
        }

        static Logger GetLogger(string name)
        {
            return NLogBuilder.ConfigureNLog("NLog-Part.config").GetLogger(name);
        }


        static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
                .AddLogging(loggingBuilder =>
                    {
                        // configure Logging with NLog
                        loggingBuilder.ClearProviders();
                        loggingBuilder.AddNLog(config);
                    })
                .BuildServiceProvider();
        }
    }
}
