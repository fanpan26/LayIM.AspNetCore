using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models.Base
{
    public class BaseModel
    {
        public string id { get; set; }
    }
    public class BaseAvatarModel : BaseModel
    {
        public string avatar { get; set; }
    }
}
