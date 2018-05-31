using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Dispatcher;
using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.Core.Models;
using LayIM.AspNetCore.Core.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Routes
{
    internal sealed partial class LayIMRoutes
    {
        #region 私有变量，方法等
        private static readonly RoutesCollection routes = new RoutesCollection();

        private static class ResourceDispatcherCreator
        {
            public static readonly IResourceDispatcher dispatcher = new ResourceDispatcher();
        }

        private static Lazy<ILayIMServer> api = GetLazyService<ILayIMServer>();
        private static Lazy<ILayIMStorage> storage = GetLazyService<ILayIMStorage>();
        private static Lazy<IMemoryCache> cache = GetLazyService<IMemoryCache>();
        private static Lazy<ILayIMFileUploader> uploader = GetLazyService<ILayIMFileUploader>();

        private static Lazy<TService> GetLazyService<TService>()
        {
            return new Lazy<TService>(() => LayIMServiceLocator.GetService<TService>());
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string CurrentUserId(HttpContext context)
        {
            context.Items.TryGetValue(LayIMGlobal.USER_KEY, out var userId);
            return userId?.ToString();
        }

        private static async Task<LayIMCommonResult> GetUploadResult(HttpContext context, bool isImg)
        {
            var uploadResult = await uploader.Value.Upload(context);
            if (string.IsNullOrEmpty(uploadResult?.FileUrl))
            {
                return LayIMCommonResult.Error("上传失败");
            }
            object result;
            if (isImg)
            {
                result = new { src = uploadResult.FileUrl };
            }
            else
            {
                result = new { src = uploadResult.FileUrl, name = uploadResult.FileName };
            }
            return LayIMCommonResult.Result(result);
        }

        private static async Task<LayIMCommonResult> GetInitData(HttpContext context)
        {
            var res = await storage.Value.GetInitData(CurrentUserId(context));
            return LayIMCommonResult.Result(res);
        }
        #endregion
    }
}
