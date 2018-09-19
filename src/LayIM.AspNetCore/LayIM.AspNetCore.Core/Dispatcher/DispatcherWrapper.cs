using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    internal sealed class DispatcherWrapper
    {
        public static readonly DispatcherWrapper Wrapper = new DispatcherWrapper();

        private DispatcherWrapper()
        {
        }
        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetCurrentUserId(HttpContext context)
        {
            var userFactory = LayIMServiceLocator.GetService<ILayIMUserFactory>();
            return userFactory.GetUserId(context);
        }

        public async Task Dispatch(ILayIMDispatcher dispatcher, HttpContext context)
        {
            string currentUserId = GetCurrentUserId(context);
            if (string.IsNullOrEmpty(currentUserId))
            {
                context.Response.ContentType = "application/json;charset=utf-8;";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(JsonUtil.ToJSON(LayIMCommonResult.Error("Unauthorized")));
                return;
            }
            //save current user id in http context
            context.Items.TryAdd(LayIMGlobal.USER_KEY, currentUserId);
            await dispatcher.Dispatch(context);
        }
    }
}
