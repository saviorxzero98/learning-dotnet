using DatabaseManager.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FluentMigrationsConsoleSample
{
    public class DatabaseMigrator
    {
        public string DatabaseType { get; set; }

        public string ConnectorString { get; set; }


        public DatabaseMigrator()
        {

        }
        public DatabaseMigrator(string databaseType, string connectorString)
        {
            DatabaseType = databaseType;
            ConnectorString = connectorString;
        }


        public void StartFluentMigrator()
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        public IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb =>
                {
                    // 加入支援的資料庫類型
                    switch (DatabaseType.ToLower())
                    {
                        case "sqlite":
                        case "sqlite3":
                            rb.AddSQLite();
                            break;

                        case "mssql":
                        case "sqlserver":
                            rb.AddSqlServer();
                            break;

                        case "postgresql":
                        case "postgre":
                        case "pgsql":
                            rb.AddPostgres();
                            break;

                        default:
                            throw new Exception("Not support this database.");
                    }


                    rb.WithGlobalConnectionString(ConnectorString)
                      .ScanIn(typeof(InitializeDatabase).Assembly)
                      .For
                      .Migrations();
                })
                .BuildServiceProvider(false);
        }

        public void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}
