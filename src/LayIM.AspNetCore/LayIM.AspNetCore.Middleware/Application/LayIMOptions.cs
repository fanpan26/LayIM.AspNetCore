using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    public class LayIMOptions
    {
        /// <summary>
        /// LayIM接口前缀，可以自定义
        /// </summary>
        public string ApiPrefix { get; set; } = "/layim";
    }
}
