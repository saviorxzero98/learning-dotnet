using System.Threading;
using System.Threading.Tasks;

namespace MediatAlt
{
    public interface IEventHandler
    {

    }

    public interface IEventHandler<in TEvent> : IEventHandler
    {
        Task HandleAsync(TEvent context, CancellationToken? cancellationToken = default);
    }
}
