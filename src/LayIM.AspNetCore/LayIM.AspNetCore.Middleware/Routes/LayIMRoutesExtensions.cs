using LayIM.AspNetCore.Middleware.Dispatcher;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Middleware.Routes
{
    internal static class LayIMRoutesExtensions
    {
        /// <summary>
        /// 注册返回值为boolean类型的命令路由
        /// </summary>
        /// <param name="routes">当前路由集合</param>
        /// <param name="path">路径</param>
        /// <param name="command">执行命令</param>
        public static void AddBooleanCommand(this RoutesCollection routes, string path, Func<HttpContext, bool> command)
        {
            Error.ThrowIfNull(path, nameof(path));
            Error.ThrowIfNull(command, nameof(command));

            routes.Add(path, new BooleanCommandDispatcher(command));
        }

        /// <summary>
        /// 注册返回值为TResult类型的命令路由
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="routes">当前路有集合</param>
        /// <param name="path">路径</param>
        /// <param name="command">执行命令</param>
        public static void AddQueryCommand<TResult>(this RoutesCollection routes, string path, Func<HttpContext, TResult> command)
        {
            Error.ThrowIfNull(path, nameof(path));
            Error.ThrowIfNull(command, nameof(command));

            routes.Add(path, new QueryCommandDispatcher<TResult>(command));
        }
    }

}
