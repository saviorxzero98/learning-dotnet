using Microsoft.Extensions.DependencyInjection;

namespace HostedServiceSample.BackgroundJobs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBackgroundJobQueue(this IServiceCollection services)
        {
            services.AddSingleton<BackgroundJobQueue>();
            services.AddHostedService<BackgroundJobService>();
            
            return services;
        }
    }
}
