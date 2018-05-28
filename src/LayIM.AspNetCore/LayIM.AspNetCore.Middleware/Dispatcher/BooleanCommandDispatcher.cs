using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace LayIM.AspNetCore.Middleware.Dispatcher
{
    internal class BooleanCommandDispatcher : CommandDispatcher<bool>
    {

        private readonly Func<HttpContext, bool> executeFunction;

        public BooleanCommandDispatcher(Func<HttpContext, bool> executeFunction)
        {
            this.executeFunction = executeFunction;
        }

        protected override Func<HttpContext, bool> ExecuteFunction => executeFunction;

        protected override string AllowMethod => HttpPost;
    }
}
