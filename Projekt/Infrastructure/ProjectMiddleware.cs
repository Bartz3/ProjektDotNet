using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Projekt.Infrastructure
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ProjectMiddleware
    {
        private readonly RequestDelegate _next;

        public ProjectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string header;
            header = httpContext.Request.Headers["User-Agent"];
            if (header.ToLower().Contains("opera"))
            {
                httpContext.Response.StatusCode = 403;
                return httpContext.Response.WriteAsync("");
            }
            else
            {
                return _next(httpContext);
            }
            //return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ProjectMiddlewareExtensions
    {
        public static IApplicationBuilder UseProjectMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ProjectMiddleware>();
        }
    }
}
