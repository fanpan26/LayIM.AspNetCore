using LayIM.AspNetCore.Core.IM;
using System;
using System.Collections.Generic;
using System.Text;
using LayIM.AspNetCore.Core.Models;
using WebApiClient;

namespace LayIM.AspNetCore.RongCloud
{
    public class RongCloudServer : ILayIMServer
    {

        private IRongCloudApi client;
        private readonly IApiActionFilter filter;

        public RongCloudServer(IApiActionFilter filter)
        {
            this.filter = filter;
            InitClient();
        }

        private void InitClient()
        {
            var config = new HttpApiConfig();
            config.GlobalFilters.Add(filter);
            client = HttpApiClient.Create<IRongCloudApi>(config);
        }

        public TokenResult GetToken(string userId)
        {
            return client.GetToken(userId).GetAwaiter().GetResult();
        }

        public LayIMBaseResult SendGroupMessage(string targetId, object message)
        {
            throw new NotImplementedException();
        }

        public LayIMBaseResult SendPrivateMessage(string targetId, object message)
        {
            throw new NotImplementedException();
        }

        public LayIMBaseResult SendSystemMessage(string targetId, object message)
        {
            throw new NotImplementedException();
        }
    }
}
