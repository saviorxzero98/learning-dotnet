using Hangfire;
using Hangfire.Console;
using Hangfire.LiteDB;
using Hangfire.MemoryStorage;
using Hangfire.PostgreSql;
using Hangfire.SQLite;
using Hangfire.SqlServer;
using HangfireSample.JobFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HangfireSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false)
                    .AddNewtonsoftJson()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // 加入 Hangfire
            services.AddHangfire((provider, configuration) => {
                // Use Condole
                configuration.UseConsole(new ConsoleOptions()
                {
                    ExpireIn = TimeSpan.FromDays(7)
                });

                // TODO: 選擇使用 Storage
                UseMemoryStorage(configuration);
                //UseSqliteStorage(configuration, "Data Source=Database/Hangfire.sqlite;");
                //UseLiteDBStorage(configuration, "Filename=Database/HangfireLite.db");

                // Add Filter
                configuration.UseFilter(new SingleJobAttribute(provider));
            });

            // 設定 Hangfire Queue
            services.AddHangfireServer((options) =>
            {
                options.Queues = new string[] { "custom" };
            });
        }

        /// <summary>
        /// [Hangfire] Use Memory Storage
        /// </summary>
        /// <param name="configuration"></param>
        public void UseMemoryStorage(IGlobalConfiguration configuration)
        {
            configuration.UseMemoryStorage(new MemoryStorageOptions() 
            {
                JobExpirationCheckInterval = TimeSpan.FromSeconds(500),
                FetchNextJobTimeout = TimeSpan.FromSeconds(500)
            });
        }

        /// <summary>
        /// [Hangfire] Use Sql Server Storage
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionString"></param>
        public void UseSqlServerStorage(IGlobalConfiguration configuration, string connectionString)
        {
            var options = new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            };

            configuration.UseSqlServerStorage(connectionString, options);
        }

        /// <summary>
        /// [Hangfire] Use PostgreSql Storage
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionString"></param>
        public void UsePostgreSqlStorage(IGlobalConfiguration configuration, string connectionString)
        {
            var options = new PostgreSqlStorageOptions
            {
                QueuePollInterval = TimeSpan.Zero,
            };

            configuration.UsePostgreSqlStorage(connectionString);
        }

        /// <summary>
        /// [Hangfire] Use SQLite Storage
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionString"></param>
        public void UseSqliteStorage(IGlobalConfiguration configuration, string connectionString)
        {
            configuration.UseSQLiteStorage(connectionString);
        }

        /// <summary>
        /// [Hangfire] Use LiteDB Storage
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionString"></param>
        public void UseLiteDBStorage(IGlobalConfiguration configuration, string connectionString)
        {
            configuration.UseLiteDbStorage(connectionString);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            GlobalJobFilters.Filters.Add(new CustomAutoRetryAttribute());

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // TODO:
            app.UseHangfireServer();        // Hangfire Server (必要)
            app.UseHangfireDashboard();     // Hangfire Dashboard (非必要)

            app.UseMvc();
        }
    }
}
