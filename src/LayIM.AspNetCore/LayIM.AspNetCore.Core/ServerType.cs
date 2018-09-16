using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core
{
    public enum ServerType
    {
        /// <summary>
        /// 融云第三方
        /// </summary>
        RongCloud = 1,
        /// <summary>
        /// SignalR .NET Core
        /// </summary>
        SignalR = 2,
        /// <summary>
        /// tio JAVA某通讯框架
        /// </summary>
        Tio = 3
    }
}
