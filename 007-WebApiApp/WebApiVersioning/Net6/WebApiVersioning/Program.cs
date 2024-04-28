using Microsoft.AspNetCore;
using NLog.Web;

namespace WebApiVersioning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLogBuilder.ConfigureNLog("NLog.config");
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .UseWebRoot("wwwroot")
                          .UseStartup<Startup>()
                          .UseNLog();
        }
    }
}
