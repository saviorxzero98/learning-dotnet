using Azure.Messaging.ServiceBus;

namespace ServiceBusSample.JobHandlers
{
    public interface IMessageQueueHandler
    {
        Task MessageHandlerAsync(ProcessMessageEventArgs args);

        Task ErrorHandlerAsync(ProcessErrorEventArgs args);
    }
}
