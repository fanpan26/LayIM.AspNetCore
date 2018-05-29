using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    public class LayIMOptions
    {
        private static readonly LayIMConfig defaultConfig = new LayIMConfig();
        /// <summary>
        /// LayIM接口前缀，可以自定义
        /// </summary>
        public string ApiPrefix { get; set; } = "/layim";
        /// <summary>
        /// 对应界面配置
        /// </summary>
        public LayIMConfig UIConfig { get; set; } = defaultConfig;
    }
}
