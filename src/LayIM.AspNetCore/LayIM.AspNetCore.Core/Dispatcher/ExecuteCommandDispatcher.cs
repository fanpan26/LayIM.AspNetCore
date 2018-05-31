using LayIM.AspNetCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    internal class ExecuteCommandDispatcher<TResult> : CommandDispatcher<TResult>
    {
        protected override Func<HttpContext, Task<TResult>> ExecuteFunction => executeFunction;

        protected override string AllowMethod => HttpPost;

        public ExecuteCommandDispatcher(Func<HttpContext, Task<TResult>> executeFunction)
        {
            this.executeFunction = executeFunction;
        }

        private readonly Func<HttpContext, Task<TResult>> executeFunction;
    }
}
