using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.WebDemo.Middleware
{
    public class HelloWorldMiddleWare
    {
        private readonly RequestDelegate next;
        public HelloWorldMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("hello world");
        }
    }

    public static class HelloWorldExtensions
    {
        public static void UseHelloWorld(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<HelloWorldMiddleWare>();
        }
    }
}
