using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.IM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LayIMServiceExtensions
    {
        public static void AddLayIM(this IServiceCollection service, Action<LayIMServiceConfig> setConfig = null)
        {
            var config = new LayIMServiceConfig();
            setConfig?.Invoke(config);
            if (config.UseRongCloud) {
               // service.AddSingleton<ILayIMServer,>();
            }
        }
    }
}
