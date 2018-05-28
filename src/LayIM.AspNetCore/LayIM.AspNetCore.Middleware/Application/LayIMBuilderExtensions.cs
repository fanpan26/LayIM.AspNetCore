using LayIM.AspNetCore.Middleware.Application;
using System;
using System.Collections.Generic;
using System.Text;


namespace Microsoft.AspNetCore.Builder
{
    public static class LayIMBuilderExtensions
    {
        public static IApplicationBuilder UseLayIM(this IApplicationBuilder app, Action<LayIMOptions> initFunc = null)
        {
            var options = new LayIMOptions();
            initFunc?.Invoke(options);
            return app.UseMiddleware<LayIMMiddleware>(options);
        }
    }
}

