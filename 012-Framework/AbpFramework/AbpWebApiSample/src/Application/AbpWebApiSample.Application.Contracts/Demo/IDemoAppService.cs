using Volo.Abp.Application.Services;

namespace AbpWebApiSample.Application.Contracts.Demo
{
    public interface IDemoAppService : IApplicationService
    {
        Task<string> GetAsync();
    }
}
