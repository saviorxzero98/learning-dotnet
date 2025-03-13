using DistributedCachingSample.Caching;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DistributedCachingSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services);


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            ConfigureCaching(services);
        }

        public static void ConfigureCaching(IServiceCollection services)
        {
            services.TryAdd(ServiceDescriptor.Singleton<ICacheManager, HybridCacheManager>());
            //services.TryAdd(ServiceDescriptor.Singleton<ICacheManager, DistributedCacheManager>());

            // Memory Cache
            //services.AddDistributedMemoryCache();

            // Redis Cache
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "localhost:6379";
            //});

            // Hybrid Cache
            services.AddHybridCache(options =>
            {
                options.DisableCompression = true;
            });
        }
    }
}
