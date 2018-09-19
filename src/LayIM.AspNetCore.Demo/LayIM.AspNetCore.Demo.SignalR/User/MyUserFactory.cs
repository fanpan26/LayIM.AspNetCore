using LayIM.AspNetCore.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace LayIM.AspNetCore.Demo.SignalR.User
{
    public class MyUserFactory : ILayIMUserFactory
    {
        /// <summary>
        /// 业务方自定义用户ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetUserId(HttpContext context)
        {
            context.Session.TryGetValue("layim_uid", out var userId);
            return userId == null ? null : Encoding.Default.GetString(userId);
        }
    }
}
