using LayIM.AspNetCore.Core.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LayIMServiceExtensions
    {
        public static void AddLayIM(IServiceCollection services, ILayIMUserFactory factory)
        {
            services.AddSingleton(factory);
        }
    }
}
