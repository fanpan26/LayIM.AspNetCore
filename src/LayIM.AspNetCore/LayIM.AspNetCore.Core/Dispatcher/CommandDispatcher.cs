using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    internal abstract class CommandDispatcher<TResult> : MethodFilterDispatcher
    {

        protected abstract Func<HttpContext, Task<TResult>> ExecuteFunction { get; }

        protected virtual void SetContentType(HttpContext context)
        {
            context.Response.ContentType = "application/json;charset=utf-8";
        }

        protected override async Task DispatchInternal(HttpContext context)
        {
            var result = await ExecuteFunction(context);

            SetContentType(context);

            await context.Response.WriteAsync(JsonUtil.ToJSON(result));
        }
        
    }
}
