using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DotnetPiper
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;
        public SecurityMiddleware()
        {
        }

        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IConfiguration configuration)
        {
            string agent = httpContext.Request.Headers["user-agent"].ToString();

            if (!string.IsNullOrEmpty(httpContext.Request.QueryString.Value))
            {
                httpContext.Response.Headers.Add("X-Frame-Options", "localhost");
                httpContext.Response.Headers.Add("X-Content-Type-Options", configuration["CustomMessage"]);
                 await httpContext.Response.WriteAsync("Query string is not allowed in Middleware pipeline   ");
                //await _next(httpContext);
                return;
            }
            else
                 await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SecurityMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityMiddleware>();
        }
    }
}
