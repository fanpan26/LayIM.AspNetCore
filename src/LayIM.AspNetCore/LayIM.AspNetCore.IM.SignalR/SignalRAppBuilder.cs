using LayIM.AspNetCore.Core;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.IM.SignalR
{
    public class SignalRAppBuilder : ILayIMAppBuilder
    {
        public void Init(IApplicationBuilder builder)
        {
            builder.UseSignalR(route => {
                route.MapHub<LayIMHub>("/layimHub");
            });
        }
    }
}
