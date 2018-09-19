using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models.Messages
{
    public enum LayIMMessageType
    {
        System = 0,
        /// <summary>
        /// 点对点消息
        /// </summary>
        ClientToClient = 1,
        /// <summary>
        /// 群组消息
        /// </summary>
        ClientToGroup = 2
    }
}
