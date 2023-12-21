using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO.Compression;
using WebApiSample.CompressionProviders;

namespace WebApiSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

            //ConfigureResponseCompression(services);
        }

        public void ConfigureResponseCompression(IServiceCollection services)
        {
            // Content Encoding : gzip
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<DeflateCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
