namespace AbpWebApiSample.Domain.Data
{
    public interface IDefaultIDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
