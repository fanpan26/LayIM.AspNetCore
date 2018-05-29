using LayIM.AspNetCore.Middleware.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Middleware.Dispatcher
{
    public interface IResourceDispatcher
    {
        Task Dispatch(HttpContext context,LayIMOptions options,RequestDelegate next);
    }
}
