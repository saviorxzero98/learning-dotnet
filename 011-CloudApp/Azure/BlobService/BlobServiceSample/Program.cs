using BlobServiceSample.Samples;
using Microsoft.Extensions.Configuration;

namespace BlobServiceSample
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();

            var sample = new BlobUploadSample(configuration);
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
