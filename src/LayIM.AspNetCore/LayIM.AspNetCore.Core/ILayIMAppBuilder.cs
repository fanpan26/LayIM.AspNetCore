using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core
{
    public interface ILayIMAppBuilder
    {
        void Build(IApplicationBuilder builder);
    }
}
