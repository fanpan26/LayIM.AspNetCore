using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    public abstract class MethodFilterDispatcher : ILayIMDispatcher
    {

        protected static readonly string HttpGet = "GET";
        protected static readonly string HttpPost = "POST";

        protected abstract string AllowMethod { get; }

        public Task Dispatch(HttpContext context)
        {
            if (!context.Request.Method.Equals(AllowMethod, StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                return Task.CompletedTask;
            }
            return DispatchInternal(context);
        }

        protected abstract Task DispatchInternal(HttpContext context);
    }
}
