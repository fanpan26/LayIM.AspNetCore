using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Middleware.Routes
{
    public sealed class LayIMRoutes
    {
        private static readonly RoutesCollection routes = new RoutesCollection();
        public static RoutesCollection Routes => routes;

        static LayIMRoutes()
        {
            RegisterCommands();
            RegisterResources();
            RegisterPages();
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        private static void RegisterCommands()
        {

        }

        /// <summary>
        /// 注册页面
        /// </summary>
        private static void RegisterPages()
        {

        }

        /// <summary>
        /// 注册资源
        /// </summary>
        private static void RegisterResources()
        {

        }
    }
}
