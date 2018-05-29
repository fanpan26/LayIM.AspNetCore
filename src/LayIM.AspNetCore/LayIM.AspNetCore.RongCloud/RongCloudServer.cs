using LayIM.AspNetCore.Core.IM;
using System;
using System.Collections.Generic;
using System.Text;
using LayIM.AspNetCore.Core.Models;

namespace LayIM.AspNetCore.RongCloud
{
    public class RongCloudServer : ILayIMServer
    {
        private readonly RongCloudConfig config;
        public RongCloudServer(RongCloudConfig config)
        {
            this.config = config;
        }
        public TokenResult GetToken(string userId)
        {
            throw new NotImplementedException();
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
