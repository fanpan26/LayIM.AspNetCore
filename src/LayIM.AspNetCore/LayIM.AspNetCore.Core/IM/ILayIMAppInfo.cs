using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.IM
{
    public interface ILayIMAppInfo
    {
        string AppKey { get; set; }
        string AppSecret { get; set; }
    }
}
