using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.IM.RongCloud;
using System;
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
        public static IServiceCollection AddRongCloud(this IServiceCollection services, Action<RongCloudConfig> setConfig)
        {
            var config = new RongCloudConfig();
            setConfig?.Invoke(config);
            return services.AddRongCloud(config);

        }

        public static IServiceCollection AddRongCloud(this IServiceCollection services, RongCloudConfig config)
        {
            services.AddSingleton<ILayIMAppInfo>(config);
            services.AddSingleton<ILayIMServer, RongCloudServer>();
            services.AddSingleton<IApiActionFilter, ApiFilter>();
            return services;
        }
    }
}
