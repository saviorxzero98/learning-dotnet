using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitWebApiClient;
using RefitWebApiClient.Extensions;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Users;
using RefitWebApiCore.RestServices;

namespace RefitWebApiClientSample
{
    internal class Program
    {
        static ServiceProvider Services;

        static async Task Main(string[] args)
        {
            Services = ConfigurationServices();


            await DemoUserAppServiceAsync();
        }

        static async Task DemoUserAppServiceAsync()
        {
            var factory = Services.GetRequiredService<IRefiRestServiceFactory>();

            // 取得使用者 Token (Post Form)
            var userService = factory.Create<IUserRestService>();
            var token = await userService.GenerateTokenAsync(new UserLoginRequest("abc", "123"));

            // 取得使用者列表
            var headerContext = new RefitHttpApiHeaderContext()
            {
                Authorization = token.Token
            };
            userService = factory.Create<IUserRestService>(headerContext);
            var users = await userService.GetListAsync(new PageDataQuery(0, 10));

            // 上傳檔案
            using (var stream = File.OpenRead("avatar.png"))
            {
                var avatar = await userService.UploadAvatarAsync("abc", new StreamPart(stream, "avatar.png", "image/jpeg"));
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

            // 加入 Client
            var setting = config.GetSection(RefitApiSetting.SettingName).Get<RefitApiSetting>();
            services.AddRefitApiClient(setting);

            return services.BuildServiceProvider();
        }
    }
}
