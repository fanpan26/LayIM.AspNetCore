using LayIM.AspNetCore.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LayIM.AspNetCore.WebDemo.User
{
    public class MyUserFactory : ILayIMUserFactory
    {
        public string GetUserId(HttpContext context)
        {
            return context.Request.Query["uid"];
        }
    }
}
