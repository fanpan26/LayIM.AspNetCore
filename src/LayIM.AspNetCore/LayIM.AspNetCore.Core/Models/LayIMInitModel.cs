using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models
{
    public class LayIMInitModel
    {
        public MineUserModel mine { get; set; }
        public IEnumerable<FriendGroupModel> friend { get; set; }
        public IEnumerable<BigGroupModel> group { get; set; }
    }
}
