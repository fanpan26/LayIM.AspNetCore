using LayIM.AspNetCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Storage
{
    public interface ILayIMStorage
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        LayIMBaseResult<LayIMInitModel> GetInitData(string userId);
    }
}
