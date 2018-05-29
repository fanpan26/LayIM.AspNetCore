using LayIM.AspNetCore.Core.Routes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Application
{
    public class LayIMMiddleware
    {
        private readonly RequestDelegate next;
        private readonly LayIMOptions options;

        public LayIMMiddleware(RequestDelegate next, LayIMOptions options)
        {
            this.next = next;
            this.options = options;
        }


        public async Task Invoke(HttpContext context)
        {
            if (context.IsLayIMRequest(options) == false)
            {
                await next?.Invoke(context);
                return;
            }
           
            string path = context.ToRoutePath(options);

            var dispatcher = LayIMRoutes.Routes.FindDispatcher(path);
            if (dispatcher != null)
            {
                await dispatcher.Dispatch(context);
            }
            else
            {
                await LayIMRoutes.ResourceDispatcher.Dispatch(context, options, next);
            }
        }
    }
}
