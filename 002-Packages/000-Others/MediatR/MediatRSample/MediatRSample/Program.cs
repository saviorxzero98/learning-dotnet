using MediatR.NotificationPublishers;

namespace MediatRSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<Program>();
                //cfg.NotificationPublisherType = typeof(ForeachAwaitPublisher); // this will be the ServiceLifetime
                cfg.NotificationPublisherType = typeof(TaskWhenAllPublisher); // this will be the ServiceLifetime
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
