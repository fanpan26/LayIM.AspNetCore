using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.IM.SignalR
{
    public class LayIMHubOptions
    {
        public bool UseRedis => !string.IsNullOrEmpty(RedisConfiguration) || RedisConfigure != null;
        public Action<HubOptions> HubConfigure { get; set; }
        public Action<RedisOptions> RedisConfigure { get; set; }
        public string RedisConfiguration { get; set; }
    }
}
