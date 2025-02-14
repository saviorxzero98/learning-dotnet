using AbpWebApiSample.Domain.Shared;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace AbpWebApiSample.Application.Contracts
{
    [DependsOn(
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpWebApiSampleDomainSharedModule)
    )]
    public class AbpWebApiSampleApplicationContractsModule : AbpModule
    {
    }
}
