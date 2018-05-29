using LayIM.AspNetCore.Core.Application;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace Microsoft.AspNetCore.Builder
{
    public static class LayIMBuilderExtensions
    {
        private const string LayIMEmbeddedFileNamespace = "LayIM.AspNetCore.Core.Resources";

        public static IApplicationBuilder UseLayIM(this IApplicationBuilder app, Action<LayIMOptions> initFunc = null)
        {
            var options = new LayIMOptions();
            initFunc?.Invoke(options);
            app.UseMiddleware<LayIMMiddleware>(options);

            app.UseFileServer(new FileServerOptions
            {
                RequestPath = options.ApiPrefix,
                FileProvider = new EmbeddedFileProvider(typeof(LayIMBuilderExtensions).GetTypeInfo().Assembly, LayIMEmbeddedFileNamespace),
            });
            return app;
        }

        public static IApplicationBuilder UseLayIM(this IApplicationBuilder app, string prefix)
        {
            return UseLayIM(app, options =>
            {
                options.ApiPrefix = prefix;
            });
        }
    }
}

