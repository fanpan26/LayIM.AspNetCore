using LayIM.AspNetCore.Core.Dispatcher;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LayIM.AspNetCore.Core.Routes
{
    /// <summary>
    /// 路由集合
    /// </summary>
    internal class RoutesCollection
    {
        private readonly List<Tuple<string, ILayIMDispatcher>> dispatchers = new List<Tuple<string, ILayIMDispatcher>>();

        private static readonly ConcurrentDictionary<string, ILayIMDispatcher> dispatcherCache = new ConcurrentDictionary<string, ILayIMDispatcher>();
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
            dispatcherCache.TryGetValue(path, out var dispatch);
            if (dispatch != null)
            {
                return dispatch;
            }

            var match = FindDispatcherMatch(path);
            if (match == null) {
                return null;
            }

            dispatcherCache.TryAdd(path, match.Item1);
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
                var pattern = $"^{dispatcher.Item1}$" ;

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
