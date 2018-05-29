using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    public static class LayIMServiceLocator
    {
        private static IServiceProvider serviceProvider;
        public static IServiceProvider ServiceProvider => serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            LayIMServiceLocator.serviceProvider = serviceProvider;
        }

        public static TService GetService<TService>()
        {
            return (TService)serviceProvider.GetService(typeof(TService));
        }
    }
}
