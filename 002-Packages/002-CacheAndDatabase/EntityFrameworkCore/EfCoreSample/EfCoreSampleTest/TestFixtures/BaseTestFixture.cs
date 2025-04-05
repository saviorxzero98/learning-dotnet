using EfCoreSample.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EfCoreSampleTest.TestFixtures
{
    public abstract class BaseTestFixture : IDisposable
    {
        public IConfiguration Configuration { get; set; }
        public IServiceProvider ServiceProvider { get; set; }


        public BaseTestFixture()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                                                                    optional: true,
                                                                    reloadOnChange: true)
                                                      .Build();
            ServiceProvider = ConfigurateServices();

            OnInitialize(Configuration, ServiceProvider);
        }

        public virtual void Dispose()
        {
        }

        public IServiceProvider ConfigurateServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(Configuration);


            return services.BuildServiceProvider();
        }

        public abstract void OnInitialize(
            IConfiguration config,
            IServiceProvider services);
    }
}
