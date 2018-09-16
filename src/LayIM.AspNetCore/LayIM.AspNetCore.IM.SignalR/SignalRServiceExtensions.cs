using LayIM.AspNetCore.Core;
using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.IM.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class SignalRServiceExtensions
    {
        /// <summary>
        /// 使用SignalR通信
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setConfig"></param>
        public static IServiceCollection AddSignalR(this IServiceCollection services, Action<LayIMHubOptions> configure)
        {
            var options = new LayIMHubOptions();
            configure?.Invoke(options);

            services.AddConnections();
            var signalRServerBuilder = services.AddSignalR(options.HubConfigure);
            if (options.UseRedis)
            {
                signalRServerBuilder.AddRedis(options.RedisConfiguration, options.RedisConfigure);
            }
            services.AddSingleton<ILayIMAppBuilder, SignalRAppBuilder>();

            LayIMServiceLocator.SetServiceProvider(services.BuildServiceProvider());
            return services;
        }
    }

}
