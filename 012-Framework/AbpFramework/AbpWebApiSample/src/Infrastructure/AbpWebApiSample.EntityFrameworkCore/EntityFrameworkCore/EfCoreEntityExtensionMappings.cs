using Microsoft.Extensions.Configuration;
using Volo.Abp.Threading;

namespace AbpWebApiSample.EntityFrameworkCore.EntityFrameworkCore
{
    public static class EfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure(IConfiguration configuration)
        {

        }
    }
}
