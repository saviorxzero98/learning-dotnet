using FusionCacheSample.Factories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FusionCacheSample
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
            // Add Fusion Cache
            services.AddFusionCache();

            // [L1] Memory Cache
            services.AddMemoryCache();

            // [L2] Redis Cache
            var redisConnection = "localhost:6379";
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
            });
            services.AddFusionCacheStackExchangeRedisBackplane(options =>
            {
                options.Configuration = redisConnection;
            });
            services.TryAdd(ServiceDescriptor.Singleton<IFusionCacheFactory, RedisFusionCacheFactory>());
        }
    }
}
