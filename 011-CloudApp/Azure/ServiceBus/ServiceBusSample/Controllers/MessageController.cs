using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceBusSample.Models;

namespace ServiceBusSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private IConfiguration _configuration;

        public MessageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<MessageDto> GetAsync()
        {
            var connectionString = _configuration.GetValue("AzureServiceBus:ConnectionString", string.Empty);
            var queueName = _configuration.GetValue("AzureServiceBus:QueueNames:Message", "message-queue");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            await using var busClient = new ServiceBusClient(connectionString);

            await using var receiver = busClient.CreateReceiver(queueName);

            var messageContext = await receiver.ReceiveMessageAsync();
            var body = messageContext.Body.ToString();

            await receiver.CompleteMessageAsync(messageContext);

            return JsonConvert.DeserializeObject<MessageDto>(body)!;
        }

        [HttpPost]
        public async Task<string> PostAsync(MessageDto message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(MessageController));
            }

            var connectionString = _configuration.GetValue("AzureServiceBus:ConnectionString", string.Empty);
            var queueName = _configuration.GetValue("AzureServiceBus:QueueNames:Message", "message-queue");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var id = Guid.NewGuid().ToString();
            message.Id = id;

            await using var busClient = new ServiceBusClient(connectionString);
            await using var sender = busClient.CreateSender(queueName);

            var messageContext = new ServiceBusMessage(JsonConvert.SerializeObject(message));
            await sender.SendMessageAsync(messageContext);

            return id;
        }
    }
}
