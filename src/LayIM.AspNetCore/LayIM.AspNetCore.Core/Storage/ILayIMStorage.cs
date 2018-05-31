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
        /// 保存聊天记录
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<int> SaveMessage(LayIMMessageModel message);
    }
}
