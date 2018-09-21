using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.IM.SignalR
{
    public class SignalRHandler : ISignalRHandler
    {

        private readonly ILayIMUserFactory userFactory;
        private readonly ILayIMStorage storage;
        public SignalRHandler(ILayIMUserFactory userFactory, ILayIMStorage storage)
        {
            this.userFactory = userFactory;
            this.storage = storage;
        }

        public async Task DoAfterConnected(HubCallerContext context, IGroupManager groupManager)
        {
            await HandleUserGroups(context, (connectionId, groupId) => {
                return groupManager.AddToGroupAsync(connectionId, groupId);
            });
        }

        public async Task DoAfterDisConnected(HubCallerContext context, IGroupManager groupManager)
        {
            await HandleUserGroups(context, (connectionId,groupId) => {
                return groupManager.RemoveFromGroupAsync(connectionId, groupId);
            });
        }

        private async Task HandleUserGroups(HubCallerContext context, Func<string, string, Task> groupAction)
        {
            string userId = userFactory.GetUserId(context.GetHttpContext());

            var groupIds = await storage.GetGroups(userId);
            string connId = context.ConnectionId;
            foreach (long groupId in groupIds)
            {
                await groupAction(connId, groupId.ToString());
            }
        }
    }
}
