using LayIM.AspNetCore.Core.Models.Messages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.IM.SignalR
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LayIMHub : Hub<ILayIMClient>
    {
        /// <summary>
        /// OnConnected
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var context = Context.GetHttpContext();
            var toClientMessage = MessageWrapper.Wrapper(new LayIMConnectedSuccessMessage
            {
                Content = "Connected Success"
            }, LayIMMessageType.System);

            await Clients.Caller.Receive(toClientMessage);
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

        public async Task SendMessage(string toUserId, LayIMMessage message)
        {
            var toClientMessage = MessageWrapper.Wrapper(message, LayIMMessageType.ClientToClient);
            await Clients.User(toUserId).Receive(toClientMessage);
        }
    }
}
