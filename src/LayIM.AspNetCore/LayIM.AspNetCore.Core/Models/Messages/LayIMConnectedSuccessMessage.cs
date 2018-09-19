using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models.Messages
{
    public class LayIMConnectedSuccessMessage
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
