using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Security;

namespace AbpConsoleSample
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpSecurityModule)
    )]
    public class ConsoleAppModule : AbpModule
    {
    }
}
