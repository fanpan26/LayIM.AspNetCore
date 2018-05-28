using LayIM.AspNetCore.Middleware.Dispatcher;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LayIM.AspNetCore.Middleware.Routes
{
    /// <summary>
    /// 路由集合
    /// </summary>
    internal class RoutesCollection
    {
        private readonly List<Tuple<string, ILayIMDispatcher>> dispatchers = new List<Tuple<string, ILayIMDispatcher>>();

        /// <summary>
        /// 路由注册
        /// </summary>
        /// <param name="pathTemplate">路径</param>
        /// <param name="dispatcher">处理器</param>
        public void Add(string pathTemplate, ILayIMDispatcher dispatcher)
        {
            Error.ThrowIfNull(pathTemplate, nameof(pathTemplate));
            Error.ThrowIfNull(dispatcher, nameof(dispatcher));

            dispatchers.Add(new Tuple<string, ILayIMDispatcher>(pathTemplate, dispatcher));
        }


        /// <summary>
        /// 获取对应路径处理器
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ILayIMDispatcher FindDispatcher(string path)
        {
            var match = FindDispatcherMatch(path);
            if (match == null) {
                return null;
            }
            return match.Item1;
        }
        /// <summary>
        /// 通过path找到对应的Dispatcher
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private Tuple<ILayIMDispatcher, Match> FindDispatcherMatch(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "/";
            }

            foreach (var dispatcher in dispatchers)
            {
                var pattern = dispatcher.Item1;

                if (!pattern.StartsWith("^", StringComparison.OrdinalIgnoreCase))
                {
                    pattern = $"^{pattern}";
                }
                if (!pattern.EndsWith("$", StringComparison.OrdinalIgnoreCase))
                {
                    pattern += "$";
                }
                var match = Regex.Match(path, pattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

                if (match.Success)
                {
                    return new Tuple<ILayIMDispatcher, Match>(dispatcher.Item2, match);
                }
            }
            return null;
        }
    }
}
