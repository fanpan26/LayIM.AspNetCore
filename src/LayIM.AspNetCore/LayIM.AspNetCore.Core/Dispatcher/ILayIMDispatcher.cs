using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    internal interface ILayIMDispatcher
    {
        Task Dispatch(HttpContext context);
    }
}
