using LayIM.AspNetCore.Middleware.Routes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Middleware.Application
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
            if (!IsLayIMRequest(context))
            {
                await next?.Invoke(context);
                return;
            }
            string path = RemovePrefix(context);
            var dispatcher = LayIMRoutes.Routes.FindDispatcher(path);
            if (dispatcher != null)
            {
                await dispatcher.Dispatch(context);
            }
            else
            {
                // static resources goto next
                if (LayIMRoutes.IsStaticResource(path))
                {
                    await next?.Invoke(context);
                }
                else
                {
                    //not found
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
        }

        private string RemovePrefix(HttpContext context)
        {
            string path = string.Empty;
            if (!string.IsNullOrEmpty(options.ApiPrefix))
            {
                path = context.Request.Path.Value.Substring(options.ApiPrefix.Length);
            }
            else
            {
                path = context.Request.Path.Value;
            }
            return path;
        }

        /// <summary>
        /// 判断是否是layim的接口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool IsLayIMRequest(HttpContext context)
        {
            return context.Request.Path.Value.StartsWith(options.ApiPrefix, StringComparison.CurrentCultureIgnoreCase);
        }

    }
}
