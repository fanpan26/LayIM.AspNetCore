using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core
{
    internal class StringUtil
    {
        public static string ReplaceHtmlTag(string value)
        {
            if (string.IsNullOrEmpty(value)) { return value; }
            return value
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("\'", "&#39;");
        }
    }
}
