using LayIM.AspNetCore.Core.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LayIMServiceExtensions
    {
        public static IServiceCollection AddLayIM(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ILayIMUserFactory,DefaultQueryUserFactory>();
            return services;
        }
    }
}
