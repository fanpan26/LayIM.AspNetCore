using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    public class LayIMOptions
    {
        private static readonly LayIMConfig defaultConfig = new LayIMConfig();

        private string apiPrefix = "/layim";
        /// <summary>
        /// LayIM接口前缀，可以自定义
        /// </summary>
        public string ApiPrefix
        {
            get
            {
                return apiPrefix;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("apiPrefix must not be null or empty");
                }
                if (!value.StartsWith("/"))
                {
                    throw new ArgumentException("apiPrefix must start with /");
                }
                apiPrefix = value;
            }
        }
        /// <summary>
        /// 对应界面配置
        /// </summary>
        public LayIMConfig UIConfig { get; set; } = defaultConfig;

        public OtherConfig OtherConfig { get; set; } = OtherConfig.DefaultOtherConfig;

        public ILayIMUserFactory UserFactory { get; set; } = new DefaultQueryUserFactory();
    }
}
