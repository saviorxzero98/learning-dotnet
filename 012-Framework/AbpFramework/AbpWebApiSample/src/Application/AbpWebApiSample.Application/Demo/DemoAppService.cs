using AbpWebApiSample.Application.Contracts.Demo;

namespace AbpWebApiSample.Application.Demo
{
    public class DemoAppService : BaseApplicationService, IDemoAppService
    {
        public Task<string> GetAsync()
        {
            return Task.FromResult("Hello World");
        }
    }
}
