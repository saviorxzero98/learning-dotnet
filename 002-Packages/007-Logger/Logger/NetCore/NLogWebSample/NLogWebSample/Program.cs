using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using Serilog;
using System;

namespace NLogWebSample
{
    public class Program
    {
        public const string NLogConfigFile = "NLog.config";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            var logger = NLogBuilder.ConfigureNLog(NLogConfigFile).GetCurrentClassLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Get Error.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                       })
                       //.UseSerilog();
                       .UseNLog();
        }
    }
}
