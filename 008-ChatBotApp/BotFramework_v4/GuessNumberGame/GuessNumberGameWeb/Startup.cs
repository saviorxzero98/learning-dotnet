using BotGame.GuessNumberGame;
using BotGame.GuessNumberGame.Accessors;
using BotGame.GuessNumberGameWeb.Bots;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;

namespace BotGame.GuessNumberGameWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 初始化設定、Service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC
            services.AddMvc(options => options.EnableEndpointRouting = false)
                    .AddNewtonsoftJson();

            // Cookie
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
            });

            // HSTS
            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });


            // 錯誤處理 Middleware
            services.AddSingleton<IBotFrameworkHttpAdapter, MiddlewareAndErrorHandler>();

            // 加入 Bot
            services.AddTransient<IBot, GuessNumberGameBot>();

            // 設定 Bot State Storage
            ConfigureBotStorage(services);

            // 註冊 Configuration
            services.AddSingleton(Configuration);
        }

        /// <summary>
        /// 設定 Bot 狀態儲存
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected IBotDataBag ConfigureBotStorage(IServiceCollection services)
        {
            // 建立/註冊 Storage
            IStorage storage = new MemoryStorage();

            IBotDataBag botDataBag = new BotDataBag(storage);

            services.AddSingleton(botDataBag);

            return botDataBag;
        }

        /// <summary>
        ///  初始化設定
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseWebSockets();
            app.UseAuthorization();
            app.UseMvc();
        }
    }
}
