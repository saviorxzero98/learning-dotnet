using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApplicationCatchUnhandledException.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = $"{GetType().Name} catch exception. Message: {ex.Message}";
                await context.Response.WriteAsync($"Exception Middleware:\n{message}");
            }
        }
    }
}
