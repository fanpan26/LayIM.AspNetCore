using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    internal class DefaultQueryUserFactory : ILayIMUserFactory
    {
        public string GetUserId(HttpContext context)
        {
            return context.Request.Query["uid"];
        }
    }
}
