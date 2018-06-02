using LayIM.AspNetCore.Core.Models;
using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Storage
{
    public interface ILayIMStorage
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<LayIMInitModel> GetInitData(string userId);

        /// <summary>
        /// 根据群组ID获取群员列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<LayIMGroupMemberModel> GetGroupMembers(string userId,string groupId);

        /// <summary>
        /// save chat messages
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<int> SaveMessage(LayIMMessageModel message);

        /// <summary>
        /// get chat history messages
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="targetId"></param>
        /// <param name="type"></param>
        /// <param name="fromTimestamp"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<IEnumerable<ChatMessageViewModel>> GetChatMessages(string userId, string targetId, string type, long fromTimestamp, int page = 20);
    }
}
