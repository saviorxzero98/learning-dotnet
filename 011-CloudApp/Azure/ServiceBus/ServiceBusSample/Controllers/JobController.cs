using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceBusSample.JobHandlers;
using ServiceBusSample.Models;

namespace ServiceBusSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private IConfiguration _configuration;
        private ILoggerFactory _loggerFactory;

        public JobController(
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        [HttpPost]
        [Route("Dequeue")]
        public async Task DequeueAsync()
        {
            var connectionString = _configuration.GetValue("AzureServiceBus:ConnectionString", string.Empty);
            var queueName = _configuration.GetValue("AzureServiceBus:QueueNames:Job", "job-queue");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            await using var busClient = new ServiceBusClient(connectionString);

            await using var processor = busClient.CreateProcessor(queueName);

            var handler = new JobQueueHandler(processor, _loggerFactory);
            
            await handler.ExecuteAsync(1000);
        }

        [HttpPost]
        [Route("Enqueue")]
        public async Task<string> EnqueueAsync(JobDto job)
        {
            var connectionString = _configuration.GetValue("AzureServiceBus:ConnectionString", string.Empty);
            var queueName = _configuration.GetValue("AzureServiceBus:QueueNames:Job", "job-queue");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var id = Guid.NewGuid().ToString();
            job.Id = id;

            await using var busClient = new ServiceBusClient(connectionString);
            await using var sender = busClient.CreateSender(queueName);

            var messageContext = new ServiceBusMessage(JsonConvert.SerializeObject(job));
            await sender.SendMessageAsync(messageContext);

            return id;
        }

        [HttpPost]
        [Route("Enqueue/Batch")]
        public async Task<List<string>> BatchEnqueueAsync(List<JobDto> jobs)
        {
            var connectionString = _configuration.GetValue("AzureServiceBus:ConnectionString", string.Empty);
            var queueName = _configuration.GetValue("AzureServiceBus:QueueNames:Job", "job-queue");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var jobIds = new List<string>();
            foreach (var job in jobs)
            {
                var jobId = Guid.NewGuid().ToString();
                job.Id = jobId;
                jobIds.Add(jobId);
            }

            await using var busClient = new ServiceBusClient(connectionString);
            await using var sender = busClient.CreateSender(queueName);

            using var jobBatch = await sender.CreateMessageBatchAsync();
            foreach (var job in jobs)
            {
                var message = new ServiceBusMessage(JsonConvert.SerializeObject(job));
                 if (!jobBatch.TryAddMessage(message))
                {
                    throw new Exception("Enqueue is fail.");
                }
            }

            await sender.SendMessagesAsync(jobBatch);

            return jobIds;
        }
    }
}
