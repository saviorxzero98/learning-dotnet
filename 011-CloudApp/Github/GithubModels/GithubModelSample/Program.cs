using GithubModelSample.Samples;
using Microsoft.Extensions.Configuration;

namespace GithubModelSample
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();

            var sample = new OpenAiCompleteChatSample(configuration);
            //var sample = new OpenAiCompleteChatStreamingSample(configuration);
            //var sample = new AiInferenceCompleteChatSample(configuration);
            //var sample = new AiInferenceCompleteChatStreamingSample(configuration);

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
