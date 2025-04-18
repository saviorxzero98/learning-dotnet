using System.Threading.Tasks;
using System.Threading;

namespace MediatAlt
{
    public interface IRequestHandler 
    {

    }

    public interface IRequestHandler<in TRequest, TResponse> : IRequestHandler
    {
        Task<TResponse> HandleAsync(TRequest context, CancellationToken? cancellationToken = default);
    }
}
