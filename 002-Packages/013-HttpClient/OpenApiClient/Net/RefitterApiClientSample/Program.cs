using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitterApiClient;
using RefitterApiClientSample.Extensions;
using RefitterApiClientSample.HeaderHandlers;

namespace RefitterApiClientSample
{
    internal class Program
    {
        static ServiceProvider Services;

        static async Task Main(string[] args)
        {
            Services = ConfigurationServices();


            await DemoAppServiceAsync();
        }

        static async Task DemoAppServiceAsync()
        {
            var appServices = Services.GetRequiredService<IWebAPISample>();

            // 取得使用者 Token (Post Form)
            //var token = await appServices.Token();

            // 取得使用者列表
            var headerSetter = Services.GetRequiredService<IContextSetter<GenericApiHeaderContext>>();
            headerSetter.SetValue(new GenericApiHeaderContext()
            {
                Authorization = Guid.NewGuid().ToString()
            });
            var usersResponse = await appServices.UserGet("10", "0");
            var users = usersResponse.Content?.ToList();

            // 上傳檔案
            using (var stream = File.OpenRead("avatar.png"))
            {
                var avatarResponse = await appServices.Avatar("abc", new StreamPart(stream, "avatar.png", "image/jpeg"));
                var avatar = avatarResponse.Content;
            }
        }

        /// <summary>
        /// 設定 Services
        /// </summary>
        /// <returns></returns>
        static ServiceProvider ConfigurationServices()
        {
            var services = new ServiceCollection();


            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                   .Build();
            services.AddSingleton<IConfiguration>(config);

            // 加入 NornsAPI Client
            var setting = config.GetSection(RefitApiSetting.SettingName).Get<RefitApiSetting>();
            services.AddHttpApiClient(setting);

            return services.BuildServiceProvider();
        }
    }
}
