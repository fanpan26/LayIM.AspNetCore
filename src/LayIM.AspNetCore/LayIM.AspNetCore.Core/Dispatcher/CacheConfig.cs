using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    internal class CacheConfig
    {
        public string CacheKey { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
