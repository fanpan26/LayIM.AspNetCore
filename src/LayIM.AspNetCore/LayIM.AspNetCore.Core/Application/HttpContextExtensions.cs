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
        public static string GetRoutePath(this HttpContext context, LayIMOptions options)
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
            string path = context.GetRoutePath(options);
            return path.StartsWith("/js")|| path.StartsWith("/page") || path.StartsWith("/css");
        }

        /// <summary>
        /// 从form表单中取值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long Int64FormValue(this HttpContext context, string key)
        {
            string value = context.Request.Form[key];
            if (value == null) { return 0; }
            long.TryParse(value, out var result);
            return result;
        }

        public static string UserId(this HttpContext context)
        {
            context.Items.TryGetValue(LayIMGlobal.USER_KEY, out var userId);
            return userId?.ToString();
        }
    }
}
