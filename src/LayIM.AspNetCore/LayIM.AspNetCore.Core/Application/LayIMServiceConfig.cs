using LayIM.AspNetCore.Core.IM;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    public class LayIMServiceConfig
    {
        /// <summary>
        /// 是否启用融云服务作为默认通讯服务，默认为true
        /// </summary>
        public bool UseRongCloud { get; set; } = true;
        public ILayIMServer LayIMServer { get; set; }

    }
}
