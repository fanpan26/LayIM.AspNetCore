using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models.Messages
{
    public class LayIMMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}
