using MediatAlt;
using MediatAltSample.Events;

namespace MediatAltSample.Handlers
{
    public class DemoEventHandler :
        IRequestHandler<ReadDemoEvent, string>,
        IEventHandler<AddDemoEvent>
    {
        private readonly ILogger<DemoEventHandler> _logger;

        public DemoEventHandler(ILogger<DemoEventHandler> logger)
        {
            _logger = logger;
        }

        public Task<string> HandleAsync(ReadDemoEvent request, CancellationToken? cancellationToken = null)
        {
            _logger.LogDebug("Send Event");
            return Task.FromResult("Demo");
        }

        public Task HandleAsync(AddDemoEvent notification, CancellationToken? cancellationToken = null)
        {
            _logger.LogDebug("Send Event");
            return Task.CompletedTask;
        }
    }
}
