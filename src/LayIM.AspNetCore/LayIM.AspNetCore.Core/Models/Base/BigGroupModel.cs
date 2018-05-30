using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models.Base
{
    public class BigGroupModel : BaseAvatarModel
    {
        public string groupname { get; set; }
    }

    public class FriendGroupModel : BaseModel
    {
        public string groupname { get; set; }
        public int online { get; set; }
        public IEnumerable<UserModel> list { get; set; }
    }
}
