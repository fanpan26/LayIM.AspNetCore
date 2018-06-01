using LayIM.AspNetCore.Core.Dispatcher;
using LayIM.AspNetCore.Core.Razor;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Routes
{
    internal static class LayIMRoutesExtensions
    {

        /// <summary>
        /// 注册返回值为TResult类型的命令路由 QUERY
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="routes">当前路有集合</param>
        /// <param name="path">路径</param>
        /// <param name="command">执行命令</param>
        public static void AddQuery<TResult>(this RoutesCollection routes, string path, Func<HttpContext, Task<TResult>> command, CacheConfig cacheConfig = null)
        {
            Error.ThrowIfNull(path, nameof(path));
            Error.ThrowIfNull(command, nameof(command));

            routes.Add(path, new QueryCommandDispatcher<TResult>(command).WithCache(cacheConfig));
        }

        /// <summary>
        /// 注册返回值为TResult类型的命令路由 EXECUTE
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="routes"></param>
        /// <param name="path"></param>
        /// <param name="command"></param>
        public static void AddExecute<TResult>(this RoutesCollection routes, string path, Func<HttpContext, Task<TResult>> command)
        {
            Error.ThrowIfNull(path, nameof(path));
            Error.ThrowIfNull(command, nameof(command));

            routes.Add(path, new ExecuteCommandDispatcher<TResult>(command));
        }

        /// <summary>
        /// 注册路由页面
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="path"></param>
        /// <param name="pageFunc"></param>
        public static void AddRazorPage(this RoutesCollection routes, string path, Func<RazorPage> pageFunc)
        {
            Error.ThrowIfNull(path, nameof(path));
            Error.ThrowIfNull(pageFunc, nameof(pageFunc));

            routes.Add(path, new RazorPageDispatcher(pageFunc));
        }
    }

}
