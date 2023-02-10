using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApplicationCatchUnhandledException.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.ToString().ToLower().EndsWith("middleware"))
            {
                throw new Exception("Error Middleware");
            }

            await _next(context);
        }
    }
}
