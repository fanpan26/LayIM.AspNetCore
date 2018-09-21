using LayIM.AspNetCore.Core;
using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.IM.SignalR;
using LayIM.AspNetCore.IM.SignalR.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            services.AddSingleton<ILayIMServer, SignalRServer>();
            services.AddScoped<ISignalRHandler, SignalRHandler>();
            services.AddSingleton<IUserIdProvider, LayIMUserIdProvider>();


            services.AddJwtBearer();
            //简单验证
            //services.AddAuthentication(auth =>
            //{
            //    auth.DefaultScheme = "User";
            //}).AddScheme<UserAuthenticationOptions, UserAuthenticationHandler>("User", o => { });

            LayIMServiceLocator.SetServiceProvider(services.BuildServiceProvider());
            return services;
        }


        private static void AddJwtBearer(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        LifetimeValidator = (before, expires, token, param) =>
                        {
                            return expires > DateTime.UtcNow;
                        },
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SignalRSecurityKey.TOKEN_KEY))
                    };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/layimHub")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }

}
