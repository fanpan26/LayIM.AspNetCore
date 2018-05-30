using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models
{
    public class LayIMInitModel
    {
        public MineUserModel mine { get; set; }
        public IList<FriendGroupModel> friend { get; set; }
        public IList<BigGroupModel> group { get; set; }
    }
}
