using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using WebApiSample.Models;
using WebApiSampleTest.Toolkits;
using Xunit;

namespace WebApiSampleTest
{
    public class DemoControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        

        public DemoControllerTest()
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
        public void TestGetBook()
        {
            // Act
            var apiClient = new HttpApiClient(_client);
            var headers = new Dictionary<string, string>()
            {
                { "Authorization", "ABC" }
            };
            HttpResponseMessage response = apiClient.Get("api/book", headers);
            var book = response.GetContentModel<Book>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(book);
        }

        [Fact]
        public void TestGetBookWithUnauthorization()
        {
            // Act
            var apiClient = new HttpApiClient(_client);
            HttpResponseMessage response = apiClient.Get("api/book");
            var book = response.GetContentModel<Book>();

            // Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Null(book);
        }

        [Fact]
        public void TestGetVersion()
        {
            // Act
            var apiClient = new HttpApiClient(_client);
            HttpResponseMessage response = apiClient.Get("api/version");
            var message = response.GetTextContent();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("Web API (Test)", message);
        }
    }
}
