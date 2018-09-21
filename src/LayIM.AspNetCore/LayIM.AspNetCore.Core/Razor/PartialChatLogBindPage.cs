using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Extensions;
using LayIM.AspNetCore.Core.Models.Base;
using LayIM.AspNetCore.Core.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Razor
{
    internal class PartialChatLogBindPage : RazorPage
    {
        private readonly ILayIMStorage storage;
        public PartialChatLogBindPage(ILayIMStorage storage)
        {
            this.storage = storage;
        }
        public override void Execute()
        {
            var messages = GetChatMessages();
            string classMine = "layim-chat-mine";
            foreach (var item in messages)
            {
                WriteLiteral($"<li trans=\"0\" class=\"{(item.self ? classMine : "") }\" data-timestamp=\"{item.addtime}\">\r\n");
                WriteLiteral("<div class=\"layim-chat-user\">\r\n");
                WriteLiteral($"<img src=\"{item.avatar}\" />\r\n");
                if (item.self)
                {
                    WriteLiteral($"<cite><i>{item.addtime.FromTimestamp().ToTimeDetailString(2)}</i>{item.username}</cite>\r\n");
                }
                else
                {
                    WriteLiteral($"<cite><i>{item.username}</i>{item.addtime.FromTimestamp().ToTimeDetailString(2)}</cite>\r\n");
                }
                WriteLiteral("</div>\r\n");
                WriteLiteral("<div class=\"layim-chat-text\">\r\n");
                WriteLiteral($"{item.msg}");
                WriteLiteral("</div>\r\n");
                WriteLiteral("</li>\r\n");
            }
        }


        private IEnumerable<ChatMessageViewModel> GetChatMessages()
        {
            if (LayIMServiceLocator.Options.UIConfig.UseHistoryPage == false)
            {
                return null;
            }

            string type = Query("type");
            string id = Query("id");
            string stamp = Query("stamp");
            string page = Query("page");

            long.TryParse(stamp, out var timestamp);
            int.TryParse(page, out var pageInt);
            return storage.GetChatMessages(UserId(), id, type, timestamp, pageInt).Result;
        }
    }
}
