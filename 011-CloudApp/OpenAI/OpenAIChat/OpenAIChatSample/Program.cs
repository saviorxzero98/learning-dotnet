using Microsoft.Extensions.Configuration;
using OpenAIChatSample.Samples;

namespace OpenAIChatSample
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();

            var sample = new CompleteChatSample(configuration);
            //var sample = new CompleteChatStreamingSample(configuration);

            await sample.RunAsync();
        }

        static IConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                          .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
                                                          .Build();
            return configuration;
        }
    }
}
