using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models
{
    /// <summary>
    /// 对应LayIM根据群分组获取群员列表的接口
    /// </summary>
    public class LayIMGroupMemberModel
    {
        public UserModel owner { get; set; }
        public int members { get; set; }
        public IEnumerable<UserModel> list { get; set; }
    }
}
