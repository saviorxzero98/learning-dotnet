using Volo.Abp.DependencyInjection;

namespace AbpWebApiSample.Domain.Data
{
    public class NullDefaultDbSchemaMigrator : IDefaultIDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
