using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.SignalR
{
    public static class MyIClientProxyExtensions
    {

        public static Task LoginAsync(this IClientProxy clientProxy, string message, CancellationToken cancellationToken = default(CancellationToken))
        {
            return clientProxy.SendCoreAsync("LoginSuccess", new object[] { message}, cancellationToken);
        }
    }
}
