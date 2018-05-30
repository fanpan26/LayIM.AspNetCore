using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models.Base
{
    public class UserModel : BaseAvatarModel
    {
        public string username { get; set; }
        public string sign { get; set; }
    }

    public class MineUserModel : UserModel
    {
        public string status { get; set; }
    }
}
