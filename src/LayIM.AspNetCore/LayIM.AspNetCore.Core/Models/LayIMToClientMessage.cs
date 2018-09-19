using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models.Messages
{
    public class LayIMToClientMessage<TMessage>
    {
        [JsonProperty("type")]
        public LayIMMessageType Type { get; set; }
        [JsonProperty("msg")]
        public TMessage Message { get; set; }
    }
}
