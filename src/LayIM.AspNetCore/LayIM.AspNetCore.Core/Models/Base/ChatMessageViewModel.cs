using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models.Base
{
    public class ChatMessageViewModel
    {
        public bool self { get; set; }
        public long from { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public string msg { get; set; }
        public long addtime { get; set; }
    }
}
