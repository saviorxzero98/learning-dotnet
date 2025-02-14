using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AbpWebApiSample.EntityFrameworkCore.EntityFrameworkCore
{
    public class DefaultDbContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        public DefaultDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            EfCoreEntityExtensionMappings.Configure(configuration);

            var builder = new DbContextOptionsBuilder<DefaultDbContext>();

            return new DefaultDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            const string dbMigratorPostfix = "DbMigrator";
            const string efCorePostfix = "EntityFrameworkCore";
            Assembly assembly = Assembly.GetEntryAssembly();
            string assemblyName = assembly.GetName().Name;
            string basePath;
            // 從 Migrator 進行 Migration
            if (assemblyName.EndsWith(dbMigratorPostfix))
            {
                basePath = Directory.GetCurrentDirectory();
            }
            // 從 EFCore CLI Tool 進行 Migration
            else
            {
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                string dbMigratorFolderName = currentAssembly.GetName().Name.Replace(efCorePostfix, dbMigratorPostfix);
                basePath = Path.Combine(Directory.GetCurrentDirectory(), $"../{dbMigratorFolderName}/");
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
