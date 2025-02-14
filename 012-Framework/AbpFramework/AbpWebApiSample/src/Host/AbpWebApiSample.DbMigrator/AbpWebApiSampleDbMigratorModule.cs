using AbpWebApiSample.Application.Contracts;
using AbpWebApiSample.EntityFrameworkCore.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpWebApiSample.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpWebApiSampleEntityFrameworkCoreModule),
        typeof(AbpWebApiSampleApplicationContractsModule)
    )]
    public class AbpWebApiSampleDbMigratorModule : AbpModule
    {
    }
}
