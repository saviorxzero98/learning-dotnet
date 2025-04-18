using MediatAlt;
using MediatAlt.Mediators;
using MediatAltSample.Events;
using MediatAltSample.Handlers;

namespace MediatAltSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IMediator, AltMediator>();
            builder.Services.AddScoped<IRequestHandler<ReadDemoEvent, string>, DemoEventHandler>();
            builder.Services.AddScoped<IEventHandler<AddDemoEvent>, DemoEventHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
