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

        protected abstract Func<HttpContext, TResult> ExecuteFunction { get; }

        protected virtual void SetContentType(HttpContext context)
        {
            context.Response.ContentType = "application/json;charset=utf-8";
        }

        protected override Task DispatchInternal(HttpContext context)
        {
            var result = ExecuteFunction(context);
            
            SetContentType(context);

            return context.Response.WriteAsync(JsonUtil.ToJSON(result));
        }
        
    }
}
