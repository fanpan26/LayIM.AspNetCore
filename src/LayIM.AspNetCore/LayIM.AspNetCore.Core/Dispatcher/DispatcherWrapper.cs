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
            //先使用用户自定义的
            var userFactory = LayIMServiceLocator.Options.UserFactory;
            if (userFactory == null)
            {
                //在使用框架自带的
                userFactory = LayIMServiceLocator.GetService<ILayIMUserFactory>();
            }
            if (userFactory == null)
            {
                return null;
            }
            return userFactory.GetUserId(context);
        }

        public async Task Dispatch(ILayIMDispatcher dispatcher, HttpContext context)
        {
            string currentUserId = GetCurrentUserId(context);
            if (string.IsNullOrEmpty(currentUserId))
            {
                context.Response.ContentType = "application/json;charset=utf-8;";
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync(JsonUtil.ToJSON(LayIMCommonResult.Error("无效的用户ID")));
                return;
            }
            //save current user id in http context
            context.Items.TryAdd(LayIMGlobal.USER_KEY, currentUserId);
            await dispatcher.Dispatch(context);
        }
    }
}
