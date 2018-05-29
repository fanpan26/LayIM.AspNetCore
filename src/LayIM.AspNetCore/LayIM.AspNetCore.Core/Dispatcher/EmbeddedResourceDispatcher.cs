using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace LayIM.AspNetCore.Middleware.Dispatcher
{
    public class EmbeddedResourceDispatcher : MethodFilterDispatcher, IResourceDispatcher
    {
        protected override string AllowMethod => HttpGet;

        protected override Task DispatchInternal(HttpContext context)
        {
            throw new NotImplementedException("please use Dispatch(HttpContext context, RequestDelegate next) method");
        }

        public Task Dispatch(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
