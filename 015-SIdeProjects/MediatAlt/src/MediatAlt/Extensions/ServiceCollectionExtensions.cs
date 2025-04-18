using MediatAlt.Mediators;
using Microsoft.Extensions.DependencyInjection;

namespace MediatAlt.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatAlt(this IServiceCollection services)
        {
            services.AddScoped<IMediator, AltMediator>();
            return services;
        }
    }
}
