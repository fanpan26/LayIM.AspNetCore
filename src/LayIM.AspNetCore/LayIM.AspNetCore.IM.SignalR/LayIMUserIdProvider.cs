using LayIM.AspNetCore.Core.Application;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.IM.SignalR
{
    /// <summary>
    /// UserId获取
    /// </summary>
    public class LayIMUserIdProvider : IUserIdProvider
    {
        private readonly IServiceProvider serviceProvider;
        public LayIMUserIdProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string GetUserId(HubConnectionContext connection)
        {
            var userFactory = serviceProvider.GetService<ILayIMUserFactory>();
            return userFactory.GetUserId(connection.GetHttpContext());
        }
    }
}
