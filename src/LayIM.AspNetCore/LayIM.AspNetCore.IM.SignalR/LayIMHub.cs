using LayIM.AspNetCore.Core;
using LayIM.AspNetCore.Core.Application;
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

        private readonly ISignalRHandler handler;
        public LayIMHub(ISignalRHandler handler)
        {
            this.handler = handler;
        }
        /// <summary>
        /// OnConnected
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            var context = Context.GetHttpContext();
            var toClientMessage = LayIMToClientMessage<LayIMConnectedSuccessMessage>.Create(new LayIMConnectedSuccessMessage
            {
                Content = "Connected Success"
            }, LayIMMessageType.System);

            handler.DoAfterConnected(Context, Groups);

            return Clients.Caller.Receive(toClientMessage);
        }

        /// <summary>
        /// OnDisconnected
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override  Task OnDisconnectedAsync(Exception exception) => handler.DoAfterDisConnected(Context,Groups);


        public Task SendMessage(string targetId, LayIMMessage message)
        {
            if (string.IsNullOrEmpty(targetId) || message == null)
            {
                return Task.CompletedTask;
            }
            var toClientMessage = LayIMToClientMessage<LayIMMessage>.Create(message, LayIMMessageType.ClientToClient);
            if (message.Type == LayIMConst.TYPE_GROUP)
            {
                return Clients.OthersInGroup(targetId).Receive(toClientMessage);
            }
            else
            {
                return Clients.User(targetId).Receive(toClientMessage);
            }
        }
    }
}
