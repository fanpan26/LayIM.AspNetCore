using LayIM.AspNetCore.Core;
using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.IM.SignalR;
using LayIM.AspNetCore.IM.SignalR.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR;
using System;

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
            var signalRServerBuilder = services.AddSignalR(options.HubConfigure);
            if (options.UseRedis)
            {
                signalRServerBuilder.AddRedis(options.RedisConfiguration, options.RedisConfigure);
            }
            //AddSignalR must be called before registering your custom SignalR services.
            services.AddSingleton<ILayIMAppBuilder, SignalRAppBuilder>();
            services.AddSingleton<IUserIdProvider, LayIMUserIdProvider>();

            //services.AddAuthorization(ops =>
            //{
            //    ops.AddPolicy("User", policy=> {
            //        //policy.AddRequirements(new UserLoginedRequirement());
            //        policy.RequireAssertion(context =>
            //        {
            //            return context.User != null;
            //        });
            //    });
            //});

            services.AddAuthentication(auth =>
            {
                auth.DefaultScheme = "signalr";
            }).AddScheme<UserAuthenticationOptions, UserAuthenticationHandler>("signalr", o => { }); ;

            LayIMServiceLocator.SetServiceProvider(services.BuildServiceProvider());
            return services;
        }
    }

}
