using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LayIM.AspNetCore.Core.Razor;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    internal class RazorPageDispatcher : ILayIMDispatcher
    {
        protected Func<RazorPage> pageFunc;

        public RazorPageDispatcher(Func<RazorPage> pageFunc)
        {
            this.pageFunc = pageFunc;
        }
        public Task Dispatch(HttpContext context)
        {
            var page = pageFunc();
            page.Assign(context);
            return context.Response.WriteAsync(page.ToString());
        }
    }
}
