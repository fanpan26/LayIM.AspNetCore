using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using LayIM.AspNetCore.Core.Application;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    internal abstract class CommandDispatcher<TResult> : MethodFilterDispatcher
    {

        private CacheConfig cacheConfig;
        private IMemoryCache memoryCache = null;

        public CommandDispatcher<TResult> WithCache(CacheConfig cacheConfig)
        {
            this.cacheConfig = cacheConfig;
            if (UseCache)
            {
                memoryCache = LayIMServiceLocator.GetService<IMemoryCache>();
            }
            return this;
        }

        private bool UseCache => cacheConfig != null;

        protected abstract Func<HttpContext, Task<TResult>> ExecuteFunction { get; }

        protected virtual void SetContentType(HttpContext context)
        {
            context.Response.ContentType = "application/json;charset=utf-8";
        }

        protected override async Task DispatchInternal(HttpContext context)
        {
            SetContentType(context);
            string json = null;
            bool cacheMissed = true;
            string cacheKey = null;
            if (UseCache)
            {
                cacheKey = cacheConfig.CacheKey + context.UserId();

                json = memoryCache.Get<string>(cacheKey);
                if (json != null)
                {
                    cacheMissed = false;
                    await context.Response.WriteAsync(json);
                }
            }
            if (cacheMissed)
            {
                var result = await ExecuteFunction(context);
                json = JsonUtil.ToJSON(result);
                await context.Response.WriteAsync(json);
                if (UseCache)
                {
                    memoryCache.Set(cacheKey, json, DateTime.Now.AddMinutes(cacheConfig.ExpireMinutes));
                }
            }
        }
        
    }
}
