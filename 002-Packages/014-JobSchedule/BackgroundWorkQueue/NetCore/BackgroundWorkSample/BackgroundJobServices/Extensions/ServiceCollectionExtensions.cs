using Microsoft.Extensions.DependencyInjection;

namespace BackgroundJobServices.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBackgroundJobService(this IServiceCollection services)
        {
            services.AddSingleton<IBackgroundJobQueue, BackgroundJobQueue>();
            services.AddHostedService<BackgroundJobService>();
            return services;
        }
    }
}
