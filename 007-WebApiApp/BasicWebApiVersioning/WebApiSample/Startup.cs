using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
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
            ConfigureNSwagDocumentServices(services);

            //ConfigureResponseCompression(services);
        }

        /// <summary>
        /// Add OpenAPI (Swagger)
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureNSwagDocumentServices(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "VVV";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddOpenApiDocument(config =>
            {
                config.DocumentName = "v1";
                config.Version = "1.0";
                config.Title = "WebAPI Sample v1";
                config.Description = "這是一個 Web API 範例";
                config.ApiGroupNames = new[] { "1" };
            });

            services.AddOpenApiDocument(config =>
            {
                config.DocumentName = "v2";
                config.Version = "2.0";
                config.Title = "WebAPI Sample v2";
                config.Description = "這是一個 Web API 範例";
                config.ApiGroupNames = new[] { "2" };
            });
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
            ConfigureNSwagDocument(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Use OpenAPI (Swagger)
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureNSwagDocument(IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseApiVersioning();
         }
    }
}
