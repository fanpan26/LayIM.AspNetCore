using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.Core.Routes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class LayIMConfig
    {

        /// <summary>
        /// 是否允许上传图片
        /// </summary>
        [JsonProperty("uploadImage")]
        public bool UseUploadImage { get; set; } = true;

        /// <summary>
        /// 是否允许上传文件
        /// </summary>
        [JsonProperty("uploadFile")]
        public bool UseUploadFile { get; set; } = true;
        /// <summary>
        /// 是否开启工具栏音频
        /// </summary>
        [JsonProperty("isAudio")]
        public bool UseAudio { get; set; } = false;
        /// <summary>
        /// 是否开启工具栏视频
        /// </summary>
        [JsonProperty("isVideo")]
        public bool UseVideo { get; set; } = false;
        /// <summary>
        /// 是否简约模式，即主面板不展示
        /// </summary>
        [JsonProperty("brief")]
        public bool Brief { get; set; } = false;
        /// <summary>
        /// 最小化时候默认标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; } = "WebIM";

        /// <summary>
        /// 主面板相对于浏览器右侧的距离
        /// </summary>
        [JsonIgnore]
        public int MarginRight { get; set; } = 0;

        [JsonProperty("right")]
        public string MarginRightStr => $"{MarginRight}px";

        /// <summary>
        /// 最小化时面板相对于浏览器右侧的距离
        /// </summary>
        [JsonIgnore]
        public int MinMarginRight { get; set; } = 0;
        [JsonProperty("minRight")]
        public string MinMarginRightStr => $"{MinMarginRight}px";

        /// <summary>
        /// 开启好友分组
        /// </summary>
        [JsonProperty("isfriend")]
        public bool UseFriend { get; set; } = true;

        /// <summary>
        /// 开启群组
        /// </summary>
        [JsonProperty("isgroup")]
        public bool UseGroup { get; set; } = true;

        /// <summary>
        /// 是否开启浏览器桌面提醒
        /// </summary>
        [JsonProperty("notice")]
        public bool UseBrowserNotice { get; set; } = false;

        /// <summary>
        /// 是否开启消息提醒
        /// </summary>
        [JsonIgnore]
        public bool UseVoiceOnMessageReceived { get; set; } = false;

        [JsonProperty("voice")]
        internal string Voice
        {
            get
            {
                return UseVoiceOnMessageReceived ? "default.mp3" : string.Empty;
            }
        }

        /// <summary>
        /// 是否使用消息提醒页面
        /// </summary>
        [JsonIgnore]
        public bool UseMessageBoxPage { get; set; } = false;

        [JsonProperty("msgbox")]
        internal string MessageBoxPage
        {
            get
            {
                if (UseMessageBoxPage)
                {
                    return LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_PAGE_MSGBOX);
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 是否使用查找页面
        /// </summary>
        [JsonIgnore]
        public bool UseFindPage { get; set; } = false;

        [JsonProperty("find")]
        internal string FindPage
        {
            get
            {
                if (UseFindPage)
                {
                    return LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_PAGE_FIND);
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 是否使用历史消息记录页面
        /// </summary>
        [JsonIgnore]
        public bool UseHistoryPage { get; set; } = true;

        [JsonProperty("chatLog")]
        internal string HistoryPage
        {
            get
            {
                if (UseHistoryPage)
                {
                    return LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_PAGE_CHATLOG);
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 使用新的皮肤
        /// </summary>
        [JsonProperty("skin")]
        public string[] UseSkins { get; set; } = new string[0];

        /// <summary>
        /// 初始化皮肤
        /// </summary>
        [JsonProperty("initSkin")]
        public string InitSkin { get; set; }
    }

    public class OtherConfig
    {
        internal static readonly OtherConfig DefaultOtherConfig = new OtherConfig();
        /// <summary>
        /// 开启客服模式
        /// </summary>
        [JsonProperty("custom")]
        public bool UseCustomService { get; set; } = false;

        [JsonProperty("log")]
        public bool UseConsoleLog { get; set; } = true;
        /// <summary>
        /// 是否保存聊天消息
        /// </summary>
        [JsonProperty("saveMsg")]
        public bool SaveMsgAfterSend { get; set; } = true;
    }

    internal class UrlConfig
    {
        internal static readonly UrlConfig DefaultUrlConfig = new UrlConfig();

        [JsonProperty("base")]
        public string InitUrl => LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_INIT);

        [JsonProperty("member")]
        public string MemberUrl => LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_GROUP_MEMBERS);

        [JsonProperty("token")]
        public string TokenUrl => LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_IM_TOKEN);

        [JsonProperty("up_img")]
        public string UploadImageUrl => LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_UPLOAD_IMAGE);

        [JsonProperty("up_file")]
        public string UploadFileUrl => LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_UPLOAD_FILE);

        [JsonProperty("save")]
        public string SaveMsgUrl => LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_SAVE_CHAT);
    }

    internal class ExtendConfig
    {
        internal static readonly ExtendConfig DefaultExtendConfig = new ExtendConfig();

        [JsonProperty("rmlib")]
        public string RmLibJs => LayIMUrls.BuildUrl(LayIMUrls.Resources.LAYIM_JS_IM_RONG_LIB);

        [JsonProperty("signalr")]
        public string SignalRJs => LayIMUrls.BuildUrl(LayIMUrls.Resources.LAYIM_JS_IM_SIGNALR);

        [JsonProperty("protobuf")]
        public string ProtoBufJs => LayIMUrls.BuildUrl(LayIMUrls.Resources.LAYIM_JS_IM_PROTOBUF);

        [JsonProperty("socket")]
        public string SocketJs
        {
            get {
                switch (LayIMServiceLocator.Options.ServerType)
                {
                    case ServerType.RongCloud:
                        return LayIMUrls.BuildUrl(LayIMUrls.Resources.LAYIM_JS_IM_SOCKET_RONG_CLOUD);
                    case ServerType.SignalR:
                        return LayIMUrls.BuildUrl(LayIMUrls.Resources.LAYIM_JS_IM_SOCKET_SIGNALR);
                }
                throw new NotSupportedException($"not supported ServerType {LayIMServiceLocator.Options.ServerType}");
            }
        } 

        [JsonProperty("init")]
        public string InitJs => LayIMUrls.Resources.LAYIM_RESOURCE_INIT_JS;

        [JsonProperty("appKey")]
        public string AppKey =>  LayIMServiceLocator.GetService<ILayIMAppInfo>()?.AppKey ?? "";
    }
}
