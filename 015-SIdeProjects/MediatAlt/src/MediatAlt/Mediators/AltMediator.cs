using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatAlt.Mediators
{
    public class AltMediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public AltMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task PublishAsync<TEventContext>(
            TEventContext context,
            CancellationToken? cancellationToken = default)
        {
            var handler = _serviceProvider.GetRequiredService<IEventHandler<TEventContext>>();
            return handler.HandleAsync(context, cancellationToken);
        }

        public Task<TResponseContext> SendAsync<TRequestContext, TResponseContext>(
            TRequestContext context,
            CancellationToken? cancellationToken = default)
        {
            var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequestContext, TResponseContext>>();
            return handler.HandleAsync(context, cancellationToken);
        }
    }
}
