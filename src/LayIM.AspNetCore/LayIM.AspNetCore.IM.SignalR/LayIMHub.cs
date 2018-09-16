using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.IM.SignalR
{
    public class LayIMHub : Hub<ILayIMClient>
    {
        /// <summary>
        /// OnConnected
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.Receive("connected succeed");
        }

        /// <summary>
        /// OnDisconnected
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage()
        {
            await Clients.Caller.Receive("hello signalr");
        }
    }
}
