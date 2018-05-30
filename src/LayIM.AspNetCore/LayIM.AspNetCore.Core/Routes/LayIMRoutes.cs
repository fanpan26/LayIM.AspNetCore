using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Dispatcher;
using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.Core.Models;
using LayIM.AspNetCore.Core.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Routes
{
    internal sealed class LayIMRoutes
    {
        public static RoutesCollection Routes => routes;
        public static IResourceDispatcher ResourceDispatcher
            => ResourceDispatcherCreator.dispatcher;

        static LayIMRoutes()
        {
            RegisterCommands();
            RegisterPages();
        }

        #region 注册相应命令
        /// <summary>
        /// 注册命令
        /// </summary>
        private static void RegisterCommands()
        {
            //获取当前LayIM配置
            routes.AddQueryCommand("/config", context =>
            {
                return Task.FromResult(new
                {
                    config = LayIMServiceLocator.Options.UIConfig,
                    uid = CurrentUserId(context),
                    api = UrlConfig.DefaultUrlConfig,
                    other = OtherConfig.DefaultOtherConfig
                });
            });

            //layim初始化接口
            routes.AddQueryCommand("/init", async context =>
                {
                    var userId = CurrentUserId(context);
                    var cacheKey = $"layim_cache_{userId}";
                    cache.Value.TryGetValue<LayIMCommonResult>(cacheKey, out var cacheInitData);
                    if (cacheInitData == null)
                    {
                        var res = await storage.Value.GetInitData(CurrentUserId(context));
                        cacheInitData = LayIMCommonResult.Result(res);
                        cache.Value.Set(cacheKey, cacheInitData);
                    }
                    return cacheInitData;
                });

            //获取连接websocket的token
            routes.AddQueryCommand("/token", async context =>
                await api.Value.GetToken(CurrentUserId(context)));
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        private static void RegisterPages()
        {

        }
        #endregion

        #region 私有变量，方法等
        private static readonly RoutesCollection routes = new RoutesCollection();

        private static class ResourceDispatcherCreator
        {
            public static readonly IResourceDispatcher dispatcher = new ResourceDispatcher();
        }

        private static Lazy<ILayIMServer> api = GetLazyService<ILayIMServer>();
        private static Lazy<ILayIMStorage> storage = GetLazyService<ILayIMStorage>();
        private static Lazy<IMemoryCache> cache = GetLazyService<IMemoryCache>();

        private static Lazy<TService> GetLazyService<TService>()
        {
            return new Lazy<TService>(() => LayIMServiceLocator.GetService<TService>());
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string CurrentUserId(HttpContext context)
        {
            context.Items.TryGetValue(LayIMGlobal.USER_KEY, out var userId);
            return userId?.ToString();
        }
        #endregion
    }
}
