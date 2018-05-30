using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Dispatcher;
using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.Core.Models;
using LayIM.AspNetCore.Core.Storage;
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

        private static Lazy<ILayIMServer> api = new Lazy<ILayIMServer>(()=> LayIMServiceLocator.GetService<ILayIMServer>());
        private static Lazy<ILayIMStorage> storage = new Lazy<ILayIMStorage>(() => LayIMServiceLocator.GetService<ILayIMStorage>());

        private static string CurrentUserId(HttpContext context)
        {
            context.Items.TryGetValue(LayIMGlobal.USER_KEY, out var userId);
            return userId?.ToString();
        }

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
                new
                {
                    config = LayIMServiceLocator.Options.UIConfig,
                    uid = CurrentUserId(context),
                    api = UrlConfig.DefaultUrlConfig,
                    other = OtherConfig.DefaultOtherConfig
                }
            );

            //layim初始化接口
            routes.AddQueryCommand("/init", context
                => storage.Value.GetInitData(CurrentUserId(context))
            );

            //获取连接websocket的token
            routes.AddQueryCommand("/token", context =>
                 api.Value.GetToken(CurrentUserId(context)));
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        private static void RegisterPages()
        {

        }

       
    }
}
