using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace WebApplicationCatchUnhandledException.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            string message = $"{GetType().Name} catch exception. Message: {context.Exception.Message}";

            context.HttpContext
                   .Response
                   .WriteAsync($"Exception Filter:\n{message}\n");
            return Task.CompletedTask;
        }
    }
}
