using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.RongCloud;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{

    public static partial class RongCloudServiceExtensions
    {
        /// <summary>
        /// 使用融云通信
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setConfig"></param>
        public static void AddLayIM(this IServiceCollection services, Action<RongCloudConfig> setConfig)
        {
            var config = new RongCloudConfig();
            setConfig?.Invoke(config);

            AddLayIM(services, config);
        }

        public static void AddLayIM(this IServiceCollection services, RongCloudConfig config)
        {
            services.AddSingleton<ILayIMAppInfo>(config);
            services.AddSingleton<ILayIMServer, RongCloudServer>();
        }
    }
}
