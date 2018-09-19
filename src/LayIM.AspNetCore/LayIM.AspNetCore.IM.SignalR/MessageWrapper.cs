using LayIM.AspNetCore.Core.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.IM.SignalR
{
    internal class MessageWrapper
    {
        public static LayIMToClientMessage<TMessage> Wrapper<TMessage>(TMessage message, LayIMMessageType messageType)
        {
            return new LayIMToClientMessage<TMessage>
            {
                Message = message,
                Type = messageType
            };
        }
    }
}
