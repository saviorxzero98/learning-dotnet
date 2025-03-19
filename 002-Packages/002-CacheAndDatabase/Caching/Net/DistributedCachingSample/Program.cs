using Microsoft.Extensions.Caching.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Backplane.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

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
            // Memory Cache
            services.AddMemoryCache();

            // Distributed Memory Cache
            //services.AddDistributedMemoryCache();

            // Add Fusion Cache
            var redisConnection = "localhost:6379";
            services.AddFusionCache()
                    .WithSerializer(new FusionCacheNewtonsoftJsonSerializer())
                    .WithDistributedCache(
                        new RedisCache(new RedisCacheOptions { Configuration = redisConnection }))
                    .WithBackplane(
                        new RedisBackplane(new RedisBackplaneOptions { Configuration = redisConnection }));
        }
    }
}
