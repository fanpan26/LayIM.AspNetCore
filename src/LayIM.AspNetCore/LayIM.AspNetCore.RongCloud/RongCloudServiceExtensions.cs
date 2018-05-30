using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.RongCloud;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class RongCloudServiceExtensions
    {
        /// <summary>
        /// 使用融云通信
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setConfig"></param>
        public static void AddRongCloud(this IServiceCollection services, Action<RongCloudConfig> setConfig, ILayIMUserFactory factory = null)
        {
            var config = new RongCloudConfig();
            setConfig?.Invoke(config);
            services.AddRongCloud(config);
            if (factory != null)
            {
                services.AddSingleton(factory);
            }
        }

        public static void AddRongCloud(this IServiceCollection services, RongCloudConfig config)
        {
            services.AddSingleton<ILayIMAppInfo>(config);
            services.AddSingleton<ILayIMServer, RongCloudServer>();
            services.AddSingleton<IApiActionFilter, ApiFilter>();
        }
    }
}
