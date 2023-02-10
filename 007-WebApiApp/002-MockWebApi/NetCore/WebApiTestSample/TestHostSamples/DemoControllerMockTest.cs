using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiSample;
using WebApiSample.Services;
using Xunit;

namespace WebApiTestSample.TestHostSamples
{
    public class DemoControllerMockTest
    {
        private readonly TestServer Server;
        private readonly HttpClient Client;
        private readonly IConfiguration Configuration;

        private readonly Mock<IIdentifierGenerator> IdGenerator;

        // SetUp
        public DemoControllerMockTest()
        {
            // 讀取 appsettings.json
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                                                                   optional: true,
                                                                   reloadOnChange: true)
                                                      .Build();

            // 設定 Web Host，執行環境、Configutation、Web API 的 Startup 類別
            IWebHostBuilder builder = new WebHostBuilder().UseEnvironment("Test")
                                                          .UseConfiguration(Configuration)
                                                          .UseStartup<Startup>();

            // Set Mock Service
            IdGenerator = new Mock<IIdentifierGenerator>(MockBehavior.Strict);
            IdGenerator.Setup(g => g.Create())
                       .Returns("mock-id");

            // Configure Test Service
            builder.ConfigureTestServices((services) =>
            {
                services.AddSingleton<IIdentifierGenerator>(IdGenerator.Object);
            });

            // 建立 Test Server 和 Http Client
            Server = new TestServer(builder);
            Client = Server.CreateClient();
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
            string exceptionId = IdGenerator.Object.Create();
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(exceptionId, responseRaw);
        }
    }
}
