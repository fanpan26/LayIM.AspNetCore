using LayIM.AspNetCore.Core.Routes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Razor
{
    internal class ChatLogPage : RazorPage
    {
        public ChatLogPage()
        {

        }

        public override void Execute()
        {

            WriteLiteral("<head>\r\n");

            WriteLiteral("<meta charset=\"utf-8\">\r\n");
            WriteLiteral("<title>聊天记录</title>\r\n");
            WriteCss("/css/layui.css"); 
            WriteLiteral("<style>body .layim-chat-main {height: auto;}</style>\r\n");
            WriteCss("/css/chatlog.css");

            WriteLiteral("</head>\r\n");

            WriteLiteral("<div class=\"layim-chat-main\">\r\n");
            WriteLiteral("<ul id=\"LAY_view\"><div id=\"chatLogMore\" class=\"layim-chat-system\"><span>查看更多记录</span></div> </ul>\r\n");
            WriteLiteral("</div>\r\n");


            WriteJs("/js/jquery.js");
            WriteJs("/js/chatlog.js");

            WriteLiteral($"<script>chatLogParam.init('{Query("type")}', '{Query("id")}','{LayIMUrls.BuildUrl(LayIMUrls.LAYIM_ROUTE_PAGE_HISTORY)}');</script>\r\n");

        }


    }
}
