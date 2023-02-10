using Microsoft.Extensions.DependencyInjection;

namespace BackgroundWorkSample.BackgroundServices
{
    public static class BackgroundJobServiceExtensions
    {
        public static IServiceCollection AddBackgroundService(this IServiceCollection services)
        {
            services.AddSingleton<BackgroundJobQueue>();
            services.AddHostedService<BackgroundJobService>();

            return services;
        }
    }
}
