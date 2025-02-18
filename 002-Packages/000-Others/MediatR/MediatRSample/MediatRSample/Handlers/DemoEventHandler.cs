using MediatR;
using MediatRSample.Events;

namespace MediatRSample.Handlers
{
    public class DemoEventHandler :
        IRequestHandler<ReadDemoEvent, string>,
        INotificationHandler<AddDemoEvent>
    {
        private readonly ILogger<DemoEventHandler> _logger;

        public DemoEventHandler(ILogger<DemoEventHandler> logger)
        {
            _logger = logger;
        }

        public Task<string> Handle(ReadDemoEvent request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Send Event");
            return Task.FromResult("Demo");
        }

        public Task Handle(AddDemoEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Send Event");
            return Task.CompletedTask;
        }
    }
}