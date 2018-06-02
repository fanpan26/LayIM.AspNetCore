using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Dispatcher;
using LayIM.AspNetCore.Core.Extensions;
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
        private static Lazy<ILayIMFileUploader> uploader = GetLazyService<ILayIMFileUploader>();

        private static Lazy<TService> GetLazyService<TService>()
        {
            return new Lazy<TService>(() => LayIMServiceLocator.GetService<TService>());
        }

        /// <summary>
        /// 获取上传结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="isImg"></param>
        /// <returns></returns>
        private static async Task<LayIMCommonResult> GetUploadResult(HttpContext context, bool isImg)
        {
            bool userUseApi = isImg ? LayIMServiceLocator.Options.UIConfig.UseUploadImage : LayIMServiceLocator.Options.UIConfig.UseUploadFile;
            if (userUseApi == false)
            {
                return LayIMCommonResult.Error("未开启上传接口，非法的请求");
            }

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

        /// <summary>
        /// 获取初始化数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task<LayIMCommonResult> GetInitData(HttpContext context)
        {
            var res = await storage.Value.GetInitData(context.UserId());
            return LayIMCommonResult.Result(res);
        }

        /// <summary>
        /// 获取群员列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task<LayIMCommonResult> GetGroupMembers(HttpContext context)
        {
            if (LayIMServiceLocator.Options.UIConfig.UseGroup == false) {
                return LayIMCommonResult.Error("未开启群组接口");
            }
            context.Request.Query.TryGetValue("id", out var groupId);
            var res = await storage.Value.GetGroupMembers(context.UserId(), groupId);
            return LayIMCommonResult.Result(res);
        }


        /// <summary>
        /// 保存聊天消息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Task<LayIMCommonResult> SaveChatMessage(HttpContext context)
        {
            if (LayIMServiceLocator.Options.OtherConfig.SaveMsgAfterSend == false)
            {
                return Task.FromResult(LayIMCommonResult.Error("未开启上传接口"));
            }

            storage.Value.SaveMessage(new Models.Base.LayIMMessageModel
            {
                From = context.UserId(),
                To = context.Request.Form["to"],
                Type = context.Request.Form["type"],
                AddTime = DateTime.Now.ToTimestamp(),
                Msg = StringUtil.ReplaceHtmlTag(context.Request.Form["msg"])
            });
            return Task.FromResult(LayIMCommonResult.Result(1));
        }

        private static async Task<LayIMCommonResult> GetChatMessages(HttpContext context)
        {
            if (LayIMServiceLocator.Options.UIConfig.UseHistoryPage == false)
            {
                return LayIMCommonResult.Error("未开启历史消息记录接口");
            }

            var query = context.Request.Query;
            query.TryGetValue("type", out var type);
            query.TryGetValue("id", out var id);
            query.TryGetValue("stamp", out var stamp);
            query.TryGetValue("page", out var page);

            long.TryParse(stamp, out var timestamp);
            int.TryParse(page, out var pageInt);

            var msgs = await storage.Value.GetChatMessages(context.UserId(), id, type, timestamp, pageInt);
            return LayIMCommonResult.Result(msgs);
        }
        #endregion
    }
}
