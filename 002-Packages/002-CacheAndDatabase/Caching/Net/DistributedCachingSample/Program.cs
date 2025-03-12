using Microsoft.Extensions.Caching.StackExchangeRedis;

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
            services.AddDistributedMemoryCache();

            // Redis Cache
            //services.AddOptions<RedisCacheOptions>().Configure(options =>
            //{
            //    options.Configuration = "localhost:6379";
            //});
            //services.AddStackExchangeRedisCache(options =>
            //{
            //});
        }
    }
}
