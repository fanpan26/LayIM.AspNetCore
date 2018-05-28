using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Middleware.Routes
{
    internal sealed class LayIMRoutes
    {
        private static readonly RoutesCollection routes = new RoutesCollection();
        public static RoutesCollection Routes => routes;

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
            
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        private static void RegisterPages()
        {

        }

        internal static bool IsStaticResource(string path)
        {
            return path.StartsWith("/js") || path.StartsWith("/css");
        }
    }
}
