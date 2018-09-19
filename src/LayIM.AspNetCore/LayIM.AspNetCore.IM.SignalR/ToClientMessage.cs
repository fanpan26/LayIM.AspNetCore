using LayIM.AspNetCore.Core.Models;
using LayIM.AspNetCore.Core.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.IM.SignalR
{
    internal class ToClientMessage<TMessage>
    {
        public LayIMMessageType Type { get; set; }
        public TMessage Message { get; set; }
    }
}
