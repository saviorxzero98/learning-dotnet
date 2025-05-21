using Microsoft.Extensions.Configuration;

namespace BlobServiceSample.Samples
{
    public abstract class DemoSampleBase
    {
        protected readonly IConfiguration Configuration;

        public DemoSampleBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task RunAsync()
        {
            await HandleAsync();
        }

        protected abstract Task HandleAsync();
    }
}
