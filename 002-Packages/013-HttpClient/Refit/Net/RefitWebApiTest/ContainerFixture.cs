using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitWebApiServer;
using System.Reflection;

namespace RefitWebApiTest
{
    public class ContainerFixture<T>
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public TestServer TestServer { get; private set; }

        public T AppService { get; private set; }

        public ContainerFixture()
        {
            var services = new ServiceCollection();


            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                   .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                                                   .Build();
            services.AddSingleton<IConfiguration>(config);

            // 設定 Test Host
            var builder = new WebHostBuilder().UseConfiguration(config)
                                              .UseStartup<Startup>()
                                              .UseSetting(WebHostDefaults.ApplicationKey, typeof(Startup).GetTypeInfo().Assembly.GetName().Name); ;
            TestServer = new TestServer(builder);
            HttpClient httpClient = TestServer.CreateClient();

            // 建立 Rest Service
            var refitSetting = new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            };
            AppService = RestService.For<T>(httpClient, refitSetting);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
