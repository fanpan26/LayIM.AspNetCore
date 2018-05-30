using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.Dispatcher
{
    internal class QueryCommandDispatcher<TResult> : CommandDispatcher<TResult>
    {
        protected override string AllowMethod => HttpGet;

        private readonly Func<HttpContext, Task<TResult>> executeFunction;
        public QueryCommandDispatcher(Func<HttpContext,Task<TResult>> executeFunction)
        {
            this.executeFunction = executeFunction;
        }

        protected override Func<HttpContext, Task<TResult>> ExecuteFunction => executeFunction;
    }
}
