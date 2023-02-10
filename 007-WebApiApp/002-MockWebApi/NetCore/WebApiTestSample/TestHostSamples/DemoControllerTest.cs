using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiSample;
using WebApiSample.Models;
using Xunit;

namespace WebApiTestSample.TestHostSamples
{
    public class DemoControllerTest
    {
        // Test Server
        private readonly TestServer Server;
        
        // Http Clien
        private readonly HttpClient Client;

        // 組態設定
        private readonly IConfiguration Configuration;

        // SetUp
        public DemoControllerTest()
        {
            // 讀取 appsettings.json
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                                                                   optional: true,
                                                                   reloadOnChange: true)
                                                      .Build();

            // 設定 Web Host，執行環境、Configutation、Web API 的 Startup 類別
            var builder = new WebHostBuilder().UseEnvironment("Test")
                                              .UseConfiguration(Configuration)
                                              .UseStartup<Startup>();

            // 建立 Test Server 和 Http Client
            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }

        [Fact]
        public async Task TestGetVersion()
        {
            // Arrange 
            var url = "api/version";

            // Act
            var response = await Client.GetAsync(url);
            var responseRaw = await response.Content.ReadAsStringAsync();

            // Assert
            var exceptionVersion = Configuration.GetSection("Version").Value.ToString();
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(exceptionVersion, responseRaw);
        }

        [Fact]
        public async Task TestGetNewId()
        {
            // Arrange 
            var url = "api/newid";

            // Act
            var response = await Client.GetAsync(url);
            var responseRaw = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestCalcCircleArea()
        {
            // Arrange 
            var url = "api/circle";
            var data = new CircleInfo() { Radius = 10 };
            var body = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync(url, body);
            var responseRaw = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CircleInfo>(responseRaw);

            // Assert
            double exceptionArea = data.Radius * data.Radius * Math.PI;
            double exceptionCircumference = data.Radius * 2 * Math.PI;
           
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(data.Radius, result.Radius);
            Assert.Equal(exceptionArea, result.Area);
            Assert.Equal(exceptionCircumference, result.Circumference);
        }
    }
}
