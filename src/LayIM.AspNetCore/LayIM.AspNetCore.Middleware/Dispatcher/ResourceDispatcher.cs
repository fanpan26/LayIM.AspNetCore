using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LayIM.AspNetCore.Middleware.Application;

namespace LayIM.AspNetCore.Middleware.Dispatcher
{
    internal class ResourceDispatcher : IResourceDispatcher
    {
        public async Task Dispatch(HttpContext context, LayIMOptions options, RequestDelegate next)
        {
            if (context.IsLayIMResourceRequest(options))
            {
               await next?.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
