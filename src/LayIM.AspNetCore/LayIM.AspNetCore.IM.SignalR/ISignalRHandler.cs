using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.IM.SignalR
{
    public interface ISignalRHandler
    {

        /// <summary>
        ///  after Connected
        /// </summary>
        /// <param name="context"></param>
        /// <param name="groupManager"></param>
        /// <returns></returns>
        Task DoAfterConnected(HubCallerContext context, IGroupManager groupManager);

        /// <summary>
        ///  after Disconnected
        /// </summary>
        /// <param name="context"></param>
        /// <param name="groupManager"></param>
        /// <returns></returns>
        Task DoAfterDisConnected(HubCallerContext context, IGroupManager groupManager);
    }
}
