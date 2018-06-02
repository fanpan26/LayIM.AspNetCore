using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Razor
{
    internal class HtmlParser
    {
        ///// <summary>
        ///// generate html with messages
        ///// </summary>
        ///// <param name="chatMessages"></param>
        ///// <returns></returns>
        //public static string GenerateChatLogHtml(IEnumerable<ChatMessageViewModel> chatMessages)
        //{
        //    string classMine = "layim-chat-mine";
        //    foreach (var item in models)
        //    {
        //        WriteLiteral($"<li trans=\"0\" class=\"{(item.self ? classMine : "") }\" data-timestamp=\"{item.timestamp}\">\r\n");
        //        WriteLiteral("<div class=\"layim-chat-user\">\r\n");
        //        WriteLiteral($"<img src=\"{item.avatar}\" />\r\n");
        //        if (item.self)
        //        {
        //            WriteLiteral($"<cite><i>{item.addtime}</i>{item.name}</cite>\r\n");
        //        }
        //        else
        //        {
        //            WriteLiteral($"<cite><i>{item.name}</i>{item.addtime}</cite>\r\n");
        //        }
        //        WriteLiteral("</div>\r\n");
        //        WriteLiteral("<div class=\"layim-chat-text\">\r\n");
        //        WriteLiteral($"{item.content}");
        //        WriteLiteral("</div>\r\n");
        //        WriteLiteral("</li>\r\n");
        //    }
        //}
    }
}
