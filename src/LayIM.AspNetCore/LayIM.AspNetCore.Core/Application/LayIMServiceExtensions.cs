using LayIM.AspNetCore.Core.Application;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LayIMServiceExtensions
    {
        public static IServiceCollection AddLayIM(this IServiceCollection services, Func<ILayIMUserFactory> userFactory = null)
        {
            var factory = userFactory?.Invoke() ?? new DefaultQueryUserFactory();

            services.AddSingleton(factory);
            services.AddSingleton<ILayIMFileUploader, DefaultFileUploader>();
            return services;
        }
    }
}
