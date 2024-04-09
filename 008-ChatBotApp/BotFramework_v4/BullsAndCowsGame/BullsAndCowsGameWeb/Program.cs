using NLog.Web;

namespace BotGame.BullsAndCowsGameWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLogBuilder.ConfigureNLog("NLog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureAppConfiguration((hostingContext, configApp) => {
                           var env = hostingContext.HostingEnvironment;
                           configApp.SetBasePath(env.ContentRootPath);
                           configApp.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                           configApp.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                           configApp.AddEnvironmentVariables();
                       })
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                           webBuilder.UseNLog();   // ¨Ï¥ÎNLog
                       });
        }
    }
}
