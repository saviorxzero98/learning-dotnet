using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace AbpConsoleSample
{
    public class HelloWorldService : ITransientDependency
    {
        public ILogger<HelloWorldService> Logger { get; set; }

        public HelloWorldService()
        {
            Logger = NullLogger<HelloWorldService>.Instance;
        }

        public Task SayHelloAsync()
        {
            Logger.LogWarning("Hello World!");
            return Task.CompletedTask;
        }
    }
}
