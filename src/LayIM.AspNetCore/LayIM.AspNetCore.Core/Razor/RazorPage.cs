using LayIM.AspNetCore.Core.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LayIM.AspNetCore.Core.Razor
{
    internal abstract class RazorPage
    {
        /// <summary>
        /// 页面内容Builder
        /// </summary>
        private readonly StringBuilder content = new StringBuilder();
        /// <summary>
        /// 页面内容
        /// </summary>
        private string body;
        private readonly string apiPrefix;

        public RazorPage()
        {
            apiPrefix = LayIMServiceLocator.Options.ApiPrefix;
        }

        protected string ApiPrefix => apiPrefix;
       

        public abstract void Execute();

        protected HttpContext HttpContext { private get; set; }

        internal void Assign(HttpContext context)
        {
            Error.ThrowIfNull(context, nameof(context));

            HttpContext = context;
        }

        public string Query(string key)
        {
            HttpContext.Request.Query.TryGetValue(key, out var value);
            return value;
        }

        protected void WriteLiteral(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend)) { return; }
            content.Append(textToAppend);
        }

        protected virtual void WriteJs(string js)
        {
            WriteLiteral($"<script src=\"{ApiPrefix}{js}\"></script>\r\n");
        }

        protected virtual void WriteCss(string css)
        {
            WriteLiteral($"<link href=\"{ApiPrefix}{css}\" rel=\"stylesheet\">\r\n");
        }

        protected virtual void Write(object value)
        {
            if (value == null) { return; }
            var html = value;
            WriteLiteral(html?.ToString() ?? Encode(value.ToString()));
        }

        protected virtual object RenderBody()
        {
            return body;
        }
        private string TransformText(string body = null)
        {
            this.body = body;
            Execute();
            return content.ToString();
        }



        private static string Encode(string text)
        {
            return string.IsNullOrEmpty(text)
                       ? string.Empty
                       : WebUtility.HtmlEncode(text);
        }

        public override string ToString()
        {
            return TransformText();
        }
    }
}
