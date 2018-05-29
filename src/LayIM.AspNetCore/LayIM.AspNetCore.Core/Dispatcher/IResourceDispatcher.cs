using LayIM.AspNetCore.Core.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    public interface IResourceDispatcher
    {
        Task Dispatch(HttpContext context,LayIMOptions options,RequestDelegate next);
    }
}
