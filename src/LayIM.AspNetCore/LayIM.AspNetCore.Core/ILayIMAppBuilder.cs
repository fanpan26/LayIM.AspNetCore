using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core
{
    public interface ILayIMAppBuilder
    {
        void Init(IApplicationBuilder builder);
    }
}
