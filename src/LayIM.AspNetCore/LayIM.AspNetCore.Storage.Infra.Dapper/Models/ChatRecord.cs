using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Storage.Infra.Dapper.Models
{
    public class ChatRecord
    {
        public long FromId { get; set; }
        public string Msg { get; set; }
        public long AddTime { get; set; }
    }
}
