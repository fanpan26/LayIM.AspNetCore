using LayIM.AspNetCore.Core.Models;
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
    }
}
