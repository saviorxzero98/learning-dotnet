using Serilog;

namespace AbpWebApiSample.Web
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            ConfigurationSerilog();

            try
            {
                Log.Information("Starting web host.");
                var builder = WebApplication.CreateBuilder(args);
                builder.Host.AddAppSettingsSecretsJson()
                    .UseAutofac()
                    .UseSerilog();
                await builder.AddApplicationAsync<AbpWebApiSampleWebModule>();
                var app = builder.Build();
                await app.InitializeApplicationAsync();
                await app.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// ³]©w Serilog
        /// </summary>
        private static void ConfigurationSerilog()
        {
            var envConfigFile = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json";
            var configuration = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .AddJsonFile(envConfigFile, true)
                                        .Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }
    }
}
