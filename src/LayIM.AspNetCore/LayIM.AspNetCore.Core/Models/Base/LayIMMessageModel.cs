using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models.Base
{
    public class LayIMMessageModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Msg { get; set; }
        public string Type { get; set; }
        public long AddTime { get; set; }
        public bool IsVlid => !(string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || Msg == null);

        public string RoomId => string.Compare(From, To) > 0 ? $"{To}{From}" : $"{From}{To}";
    }
}
