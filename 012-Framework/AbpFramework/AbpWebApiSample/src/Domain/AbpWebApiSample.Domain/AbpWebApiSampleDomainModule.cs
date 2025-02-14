using AbpWebApiSample.Domain.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace AbpWebApiSample.Domain
{
    [DependsOn(
        typeof(AbpWebApiSampleDomainSharedModule)
    )]
    public class AbpWebApiSampleDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AbpWebApiSampleDomainModule>();
            });
        }
    }
}
