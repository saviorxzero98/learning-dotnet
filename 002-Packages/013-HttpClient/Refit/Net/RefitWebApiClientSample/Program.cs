using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitWebApiClient;
using RefitWebApiCore.AppServices;
using RefitWebApiCore.Models.Users;
using RefitWebApiCore.Models;
using RefitWebApiClient.HeaderHandlers;

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

        static async Task DemoBookAppSerivceAsync()
        {
            
        }

        static async Task DemoUserAppServiceAsync()
        {
            var appServices = Services.GetRequiredService<IUserAppService>();

            // 取得使用者 Token (Post Form)
            var token = await appServices.GenerateTokenAsync(new UserLoginRequest("abc", "123"));

            // 取得使用者列表
            var headerSetter = Services.GetRequiredService<IContextSetter<GenericApiHeaderContext>>();
            headerSetter.SetValue(new GenericApiHeaderContext()
            {
                Authorization = token.Token
            });
            var users = await appServices.GetListAsync(new PageDataQuery(0, 10));

            // 上傳檔案
            using (var stream = File.OpenRead("avatar.png"))
            {
                var avatar = await appServices.UploadAvatarAsync("abc", new StreamPart(stream, "avatar.png", "image/jpeg"));
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
            services.AddHttpApiClient(setting);

            return services.BuildServiceProvider();
        }
    }
}
