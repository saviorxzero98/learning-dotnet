using Garnet;
using Garnet.server;
using Garnet.server.Auth.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace GarnetSampleServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var options = ReadServerSettings();
                var loggerFactory = GetLoggerFactory();
                
                using (var server = new GarnetServer(options, loggerFactory))
                {
                    server.Start();
                    Thread.Sleep(Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to initialize server due to exception: {e.Message}");
            }
        }

        /// <summary>
        /// 讀取設定
        /// </summary>
        /// <returns></returns>
        static GarnetServerOptions ReadServerSettings()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                                                                           optional: true,
                                                                           reloadOnChange: true)
                                                              .Build();
            
            GarnetSetting setting = config.GetSection(GarnetSetting.SettingName).Get<GarnetSetting>() ?? new GarnetSetting();

            var options = new GarnetServerOptions()
            {
                Address = setting.Address,
                Port = setting.Port
            };

            if (!string.IsNullOrEmpty(setting.Password))
            {
                options.AuthSettings = new PasswordAuthenticationSettings(setting.Password);
            }
            return options;
        }

        static ILoggerFactory GetLoggerFactory()
        {
            return new NLogLoggerFactory();
        }
    }
}
