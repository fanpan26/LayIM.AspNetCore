using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Dispatcher;
using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Routes
{
    internal sealed class LayIMRoutes
    {
        private static readonly RoutesCollection routes = new RoutesCollection();
        public static RoutesCollection Routes => routes;

        private static class ResourceDispatcherCreator
        {
            public static readonly IResourceDispatcher dispatcher = new ResourceDispatcher();
        }

        public static IResourceDispatcher ResourceDispatcher
            => ResourceDispatcherCreator.dispatcher;
        

        static LayIMRoutes()
        {
            RegisterCommands();
            RegisterPages();
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        private static void RegisterCommands()
        {
            //获取当前LayIM配置
            routes.AddQueryCommand("/config", context =>
            {
                return LayIMServiceLocator.Options.UIConfig;
            });

            //layim初始化接口
            routes.AddQueryCommand<object>("/init", context =>
            {
                return context.Request.Query["uid"];
            });

            //获取连接websocket的token
            routes.AddQueryCommand("/token", context =>
            {
                var server = LayIMServiceLocator.GetService<ILayIMServer>();
                return server.GetToken(context.Request.Query["uid"]);
            });
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        private static void RegisterPages()
        {

        }

       
    }
}
