using AbpWebApiSample.Application.Contracts;
using AbpWebApiSample.Domain;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace AbpWebApiSample.Application
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpWebApiSampleApplicationContractsModule),
        typeof(AbpWebApiSampleDomainModule)
    )]
    public class AbpWebApiSampleApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AbpWebApiSampleApplicationModule>();
            });
        }
    }
}
