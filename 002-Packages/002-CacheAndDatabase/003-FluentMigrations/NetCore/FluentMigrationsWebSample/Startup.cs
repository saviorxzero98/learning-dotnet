using DatabaseManager.Migrations;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FluentMigrationsWebSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Initialize Database
            ConfigureDatabase(services);
        }

        public void ConfigureDatabase(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("Default");
            string databaseType = "sqlite";

            services.AddFluentMigratorCore()
                    .ConfigureRunner(rb =>
                    {
                        // 加入支援的資料庫類型
                        switch (databaseType.ToLower())
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


                        rb.WithGlobalConnectionString(connectionString)
                          .ScanIn(typeof(InitializeDatabase).Assembly)
                          .For
                          .Migrations();
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMigrationRunner migrationRunner)
        {
            // Migrate Database
            migrationRunner.MigrateUp();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
