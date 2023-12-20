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
                // �]�w���W�� (���n) (�w�]��: v1)
                config.DocumentName = "v1";

                // �]�w���� API ������T
                config.Version = "0.0.1";

                // �]�w�����D (����� Swagger/ReDoc UI ���ɭԷ|��ܦb�e���W)
                config.Title = "WebApi Sample";

                // �]�w���²�n����
                config.Description = "�o�O�@�� Web API �d��";
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
