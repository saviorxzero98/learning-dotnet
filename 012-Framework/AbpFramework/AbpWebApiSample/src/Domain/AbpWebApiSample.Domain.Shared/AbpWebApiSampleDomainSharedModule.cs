using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace AbpWebApiSample.Domain.Shared
{
    [DependsOn(
        typeof(AbpDddDomainSharedModule)
    )]
    public class AbpWebApiSampleDomainSharedModule : AbpModule
    {
    }
}
