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
        public void TestGetCountry()
        {
            // Act
            var apiClient = new HttpApiClient(_client);
            var headers = new Dictionary<string, string>()
            {
                { "Authorization", "ABC" }
            };
            HttpResponseMessage response = apiClient.Get("api/country", headers);
            var country = response.GetContentModel<CountryProfile>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(country);
        }

        [Fact]
        public void TestGetCountryWithUnauthorization()
        {
            // Act
            var apiClient = new HttpApiClient(_client);
            HttpResponseMessage response = apiClient.Get("api/country");
            var country = response.GetContentModel<CountryProfile>();

            // Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Null(country);
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
