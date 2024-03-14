using RefitWebApiCore.RestServices;
using RefitWebApiServer.AppServices;

namespace RefitWebApiServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            // Add OpenAPI (Swagger)
            services.AddOpenApiDocument(config =>
            {
                config.DocumentName = "v1";
                config.Version = "0.0.1";
                config.Title = "WebAPI Sample";
                config.Description = "這是一個 Web API 範例";
            });

            // Add Application Service
            services.AddTransient<IBookRestService, BookRestService>();
            services.AddTransient<IUserRestService, UserRestService>();
        }

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
            app.UseHttpsRedirection();
            app.UseRouting();

            // Use OpenAPI (Swagger)
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(config =>
            {
                config.Path = "/redoc";
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
