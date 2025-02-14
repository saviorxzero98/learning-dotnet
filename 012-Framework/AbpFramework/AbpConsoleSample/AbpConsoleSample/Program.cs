﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace AbpConsoleSample
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
               .MinimumLevel.Debug()
#else
           .MinimumLevel.Information()
#endif
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               //.WriteTo.Async(c => c.File("Logs/logs.txt"))
               .WriteTo.Async(c => c.Console())
               .CreateLogger();

            try
            {
                Log.Information("Starting console host.");

                var builder = Host.CreateApplicationBuilder(args);

                await ConfigureServicesAsync(builder);

                var host = builder.Build();

                await host.InitializeAsync();

                await host.RunAsync();

                return 0;
            }
            catch (Exception ex)
            {
                if (ex is HostAbortedException)
                {
                    throw;
                }

                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static async Task ConfigureServicesAsync(IHostApplicationBuilder builder)
        {
            builder.Configuration.AddAppSettingsSecretsJson();
            builder.Logging.ClearProviders().AddSerilog();

            builder.ConfigureContainer(builder.Services.AddAutofacServiceProviderFactory());

            builder.Services.AddHostedService<ConsoleAppHostedService>();

            await builder.Services.AddApplicationAsync<ConsoleAppModule>();
        }
    }
}
