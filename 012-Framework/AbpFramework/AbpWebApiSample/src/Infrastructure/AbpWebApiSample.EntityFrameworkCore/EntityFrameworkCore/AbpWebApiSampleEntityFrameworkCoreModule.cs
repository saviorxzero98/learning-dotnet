using AbpWebApiSample.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AbpWebApiSample.EntityFrameworkCore.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpWebApiSampleDomainModule)
    )]
    public class AbpWebApiSampleEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EfCoreEntityExtensionMappings.Configure(context.Services.GetConfiguration());
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDbContextOptions>(options =>
            {
                // TODO: Change Database
                options.UseSqlServer();
            });
        }
    }
}
