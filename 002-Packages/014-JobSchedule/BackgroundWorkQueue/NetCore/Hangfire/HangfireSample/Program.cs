using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HangfireSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            // 自訂 Hosting 設定
            //var config = new ConfigurationBuilder()
            //                    .SetBasePath(Directory.GetCurrentDirectory())
            //                    .AddJsonFile("hosting.json", true)
            //                    .Build();

            return WebHost.CreateDefaultBuilder(args)
                          //.UseConfiguration(config)     // 使用自訂 Hosting 設定
                          .UseStartup<Startup>()
                          .UseWebRoot("wwwroot");       // 使用NLog
        }
    }
}
