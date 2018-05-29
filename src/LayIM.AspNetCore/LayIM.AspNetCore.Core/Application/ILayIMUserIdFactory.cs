using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    public interface ILayIMUserIdFactory
    {
        string GetUserId(HttpContext context);
    }
}
