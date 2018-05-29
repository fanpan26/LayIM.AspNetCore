using LayIM.AspNetCore.Core.IM;
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
        /// 是否保存聊天消息
        /// </summary>
        [JsonProperty("saveMsg")]
        public bool SaveMsgAfterSend { get; set; } = true;
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
        public int MarginRight { get; set; } = 10;

        [JsonProperty("right")]
        public string MarginRightStr => $"{MarginRight}px";

        /// <summary>
        /// 最小化时面板相对于浏览器右侧的距离
        /// </summary>
        [JsonIgnore]
        public int MinMarginRight { get; set; } = 10;
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
        [JsonProperty("voice")]
        public bool UseVoiceOnMessageReceived { get; set; } = true;

        /// <summary>
        /// 是否使用消息提醒页面
        /// </summary>
        [JsonIgnore]
        public bool UseMessageBoxPage { get; set; } = false;

        [JsonProperty("msgbox")]
        public string MessageBoxPage
        {
            get
            {
                if (UseMessageBoxPage)
                {
                    return $"{LayIMServiceLocator.Options.ApiPrefix}/html/msgbox.html";
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
        public string FindPage
        {
            get
            {
                if (UseFindPage)
                {
                    return $"{LayIMServiceLocator.Options.ApiPrefix}/html/find.html";
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
        public string HistoryPage
        {
            get
            {
                if (UseHistoryPage)
                {
                    return $"{LayIMServiceLocator.Options.ApiPrefix}/html/chatlog.html";
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

        [JsonProperty("other")]
        public OtherConfig Other { get; set; } = OtherConfig.DefaultOtherConfig;


        [JsonProperty("url")]
        public UrlConfig Url { get; set; } = UrlConfig.DefaultUrlConfig;
    }

    public class OtherConfig
    {
        internal static readonly OtherConfig DefaultOtherConfig = new OtherConfig();
        /// <summary>
        /// 开启客服模式
        /// </summary>
        [JsonProperty("custom")]
        public bool UseCustomService { get; set; } = false;

        [JsonProperty("appKey")]
        public string AppKey => LayIMServiceLocator.GetService<ILayIMAppInfo>()?.AppKey ?? "";

        [JsonProperty("log")]
        public bool UseConsoleLog { get; set; } = true;
    }

    public class UrlConfig
    {
        internal static readonly UrlConfig DefaultUrlConfig = new UrlConfig();

        [JsonProperty("base")]
        public string InitUrl => $"{LayIMServiceLocator.Options.ApiPrefix}/init";

        [JsonProperty("member")]
        public string MemberUrl => $"{LayIMServiceLocator.Options.ApiPrefix}/member";

        [JsonProperty("token")]
        public string TokenUrl => $"{LayIMServiceLocator.Options.ApiPrefix}/token";
    }
}
