using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    internal class BooleanCommandDispatcher : CommandDispatcher<bool>
    {

        private readonly Func<HttpContext,Task<bool>> executeFunction;

        public BooleanCommandDispatcher(Func<HttpContext, Task<bool>> executeFunction)
        {
            this.executeFunction = executeFunction;
        }

        protected override Func<HttpContext, Task<bool>> ExecuteFunction => executeFunction;

        protected override string AllowMethod => HttpPost;
    }
}
