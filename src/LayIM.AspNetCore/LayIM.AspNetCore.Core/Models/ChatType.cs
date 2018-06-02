using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models
{
    public enum ChatType
    {
        /// <summary>
        /// 私聊
        /// </summary>
        Friend = 1,
        /// <summary>
        /// 群聊
        /// </summary>
        Group = 2,
        /// <summary>
        /// 客服
        /// </summary>
        CustomService = 3
    }

    public static class ChatTypeExtensions
    {
        public static string ToChatTypeString(this ChatType chatType)
        {
            switch (chatType)
            {

                case ChatType.Friend:
                    return "friend";
                case ChatType.Group:
                    return "group";
                case ChatType.CustomService:
                    return "kefu";
            }
            return null;
        }
    }
}
