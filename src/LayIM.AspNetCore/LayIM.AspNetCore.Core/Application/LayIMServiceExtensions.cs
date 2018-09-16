using LayIM.AspNetCore.Core.Application;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LayIMServiceExtensions
    {
        public static IServiceCollection AddLayIM(this IServiceCollection services)
        {
            services.AddSingleton<ILayIMUserFactory,DefaultQueryUserFactory>();
            services.AddSingleton<ILayIMFileUploader, DefaultFileUploader>();
            return services;
        }
    }
}
