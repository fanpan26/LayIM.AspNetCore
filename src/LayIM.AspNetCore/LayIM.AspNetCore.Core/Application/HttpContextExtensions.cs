using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    internal static class HttpContextExtensions
    {
        /// <summary>
        /// 转换路由路径
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string ToRoutePath(this HttpContext context, LayIMOptions options)
        {
            string path = string.Empty;
            string prefix = options.ApiPrefix;

            if (!string.IsNullOrEmpty(prefix))
            {
                path = context.Request.Path.Value.Substring(prefix.Length);
            }
            else
            {
                path = context.Request.Path.Value;
            }
            return path;
        }

        /// <summary>
        /// 是否LayIM接口请求
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsLayIMRequest(this HttpContext context, LayIMOptions options)
        {
            return IsConfigPath(context.Request.Path.Value) || context.Request.Path.Value.StartsWith(options.ApiPrefix, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 是否是配置路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsConfigPath(string path)
        {
            return "/layim/config".Equals(path, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 是否LayIM静态资源请求
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsLayIMResourceRequest(this HttpContext context, LayIMOptions options)
        {
            string path = context.ToRoutePath(options);
            return path.StartsWith("/js") || path.StartsWith("/css");
        }
    }
}
