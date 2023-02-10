using Microsoft.Extensions.Configuration;
using System;

namespace FluentMigrationsConsoleSample
{
    class Program
    {
        static IConfiguration Configuration;

        static void Main(string[] args)
        {
            InitializeConfiguration();

            string connectionString = Configuration.GetConnectionString("Default");
            DatabaseMigrator migrator = new DatabaseMigrator("sqlite", connectionString);
            migrator.StartFluentMigrator();

            Console.WriteLine("Success!");
        }

        static void InitializeConfiguration()
        {
            // 1.建立 IConfiguration
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                                                                   optional: true,
                                                                   reloadOnChange: true)
                                                      .Build();
        }
    }
}
