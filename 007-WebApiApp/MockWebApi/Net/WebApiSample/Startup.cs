using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiSample.Services;

namespace WebApiSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add Swagger/OpenAPI
            ConfigureOpenApi(services);


            ConfigureIdentifierGenerator(services);
        }

        public virtual void ConfigureIdentifierGenerator(IServiceCollection services)
        {
            services.AddSingleton<IIdentifierGenerator>(new GuidGenerator());
        }

        public virtual void ConfigureOpenApi(IServiceCollection services)
        {
            services.AddOpenApiDocument(config =>
            {
                // 設定文件名稱 (重要) (預設值: v1)
                config.DocumentName = "v1";

                // 設定文件或 API 版本資訊
                config.Version = "0.0.1";

                // 設定文件標題 (當顯示 Swagger/ReDoc UI 的時候會顯示在畫面上)
                config.Title = "WebApi Sample";

                // 設定文件簡要說明
                config.Description = "這是一個 Web API 範例";
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(config =>
            {
                config.Path = "/redoc";
            });


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
