using Azure.Messaging.ServiceBus;
using System.Diagnostics;

namespace ServiceBusSample.JobHandlers
{
    public class JobQueueHandler : IMessageQueueHandler
    {
        private readonly ServiceBusProcessor _processor;
        private readonly ILogger _logger;

        public JobQueueHandler(
            ServiceBusProcessor processor,
            ILoggerFactory loggerFactory)
        {
            _processor = processor;
            _processor.ProcessMessageAsync += MessageHandlerAsync;
            _processor.ProcessErrorAsync += ErrorHandlerAsync;
            _logger = loggerFactory.CreateLogger<JobQueueHandler>();
        }


        public Task MessageHandlerAsync(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();
            
            return Task.CompletedTask;
        }

        public Task ErrorHandlerAsync(ProcessErrorEventArgs args)
        {
            var exception = args.Exception.ToString();
            _logger.LogError(exception);

            return Task.CompletedTask;
        }

        public async Task ExecuteAsync(long delayTime)
        {
            if (!_processor.IsProcessing)
            {
                await _processor.StartProcessingAsync();

                await Task.Delay(1000);

                await _processor.StopProcessingAsync();
            }
        }
    }
}
