using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LayIM.AspNetCore.Middleware.Dispatcher
{
    internal abstract class CommandDispatcher<TResult> : MethodFilterDispatcher
    {

        protected abstract Func<HttpContext, TResult> ExecuteFunction { get; }

        protected override Task DispatchInternal(HttpContext context)
        {
            var result = ExecuteFunction(context);
            return context.Response.WriteAsync(JsonUtil.ToJSON(result));
        }
        
    }
}
