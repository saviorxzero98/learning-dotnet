using Microsoft.Extensions.Hosting;

namespace AbpConsoleSample
{
    public class ConsoleAppHostedService : IHostedService
    {
        private readonly HelloWorldService _helloWorldService;

        public ConsoleAppHostedService(HelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _helloWorldService.SayHelloAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
