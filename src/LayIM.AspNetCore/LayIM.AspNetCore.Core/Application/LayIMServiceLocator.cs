using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    public static class LayIMServiceLocator
    {
        private static IServiceProvider serviceProvider;

        public static LayIMOptions Options { get; private set; }

        public static void SetOptions(LayIMOptions options)
        {
            Options = options;
        }

        public static void SetServiceProvider(IServiceProvider provider)
        {
            serviceProvider = provider;
        }


        public static TService GetService<TService>()
        {
            return (TService)serviceProvider?.GetService(typeof(TService));
        }
    }
}
