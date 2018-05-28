using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LayIM.AspNetCore.Middleware.Dispatcher
{
    public abstract class CommandDispatcher<TResult> : ILayIMDispatcher
    {
        public CommandDispatcher()
        {
        }

        protected static readonly string HttpGet = "GET";
        protected static readonly string HttpPost = "POST";

        protected abstract string AllowMethod { get; }

        protected abstract Func<HttpContext, TResult> ExecuteFunction { get; }


        public Task Dispatch(HttpContext context)
        {
            if (!context.Request.Method.Equals(AllowMethod, StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                return Task.CompletedTask;
            }
            var result = ExecuteFunction(context);
            return context.Response.WriteAsync(JsonUtil.ToJSON(result));
        }
    }
}
