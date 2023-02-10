using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BackgroundQueueSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          //.UseConfiguration(config)     // 使用自訂 Hosting 設定
                          .UseStartup<Startup>()
                          .UseWebRoot("wwwroot");                   // 使用NLog
        }
    }
}
