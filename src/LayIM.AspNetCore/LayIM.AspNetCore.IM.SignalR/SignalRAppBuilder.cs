using LayIM.AspNetCore.Core;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.IM.SignalR
{
    public class SignalRAppBuilder : ILayIMAppBuilder
    {
        public void Build(IApplicationBuilder builder)
        {
            builder.UseSignalR(route => {
                route.MapHub<LayIMHub>("/layimHub", connectionOptions =>
                {
                    //connectionOptions.ApplicationMaxBufferSize = 32 * 1024 * 8;
                    //connectionOptions.TransportMaxBufferSize = 32 * 1024 * 8;
                });
            });
        }
    }
}
