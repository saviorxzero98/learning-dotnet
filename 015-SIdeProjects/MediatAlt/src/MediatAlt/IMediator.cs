using System.Threading;
using System.Threading.Tasks;

namespace MediatAlt
{
    public interface IMediator
    {
        Task PublishAsync<TEvent>(TEvent context, CancellationToken? cancellationToken = default);

        Task<TResponse> SendAsync<TRequestContext, TResponse>(TRequestContext context, CancellationToken? cancellationToken = default);
    }
}
