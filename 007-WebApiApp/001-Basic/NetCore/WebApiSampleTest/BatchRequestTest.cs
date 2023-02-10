using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiSampleTest.Toolkits;
using Xunit;

namespace WebApiSampleTest
{
    public class BatchRequestTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;


        public BatchRequestTest()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                                                                           optional: true,
                                                                           reloadOnChange: true)
                                                              .Build();
            var builder = new WebHostBuilder().UseConfiguration(config)
                                              .UseStartup<WebApiSample.Startup>();
            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task TestRequest()
        {
            var requests = GetRequests();
            List<string> results = new List<string>();

            DateTime startDate = DateTime.Now;

            foreach (var request in requests)
            {
                var apiClient = new HttpApiClient(_client);
                HttpResponseMessage response = await apiClient.GetAsync("api/version");
                var message = response.GetTextContent();
                results.Add($"[{request.Id}] {message}");
            }

            DateTime endDate = DateTime.Now;

            double seconds = (endDate - startDate).TotalMilliseconds;
        }

        [Fact]
        public async Task TestParallelRequest()
        {
            var requests = GetRequests();
            var taskList = new List<Task<string>>();

            foreach (var request in requests)
            {
                taskList.Add(GetResponseAsync(request));
            }

            DateTime startDate = DateTime.Now;

            var results = await Task.WhenAll(taskList);

            DateTime endDate = DateTime.Now;

            double seconds = (endDate - startDate).TotalMilliseconds;
        }

        [Fact]
        public async Task TestFixedParallelRequest()
        {
            var requests = GetRequests();
            var taskList = new List<Task<string>>();
            List<string> results = new List<string>();

            var batchSize = 10;
            int numberOfBatches = (int)Math.Ceiling((double)requests.Count / batchSize);

            DateTime startDate = DateTime.Now;

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentIds = requests.Skip(i * batchSize).Take(batchSize);
                var tasks = currentIds.Select(req => GetResponseAsync(req));
                results.AddRange(await Task.WhenAll(tasks));
            }

            DateTime endDate = DateTime.Now;

            double seconds = (endDate - startDate).TotalMilliseconds;
        }

        public async Task<string> GetResponseAsync(RequestTestModel model)
        {
            var apiClient = new HttpApiClient(_client);
            HttpResponseMessage response = await apiClient.GetAsync("api/version");
            var message = response.GetTextContent();
            return $"[{model.Id}] {message}";
        }


        protected List<RequestTestModel> GetRequests()
        {
            var requests = new List<RequestTestModel>();

            for (int i = 0; i < 20; i++)
            {
                requests.Add(new RequestTestModel() { Id = $"{i}" });
            }

            return requests;
        }
    }


    public class RequestTestModel
    {
        public string Id { get; set; }
    }
}
