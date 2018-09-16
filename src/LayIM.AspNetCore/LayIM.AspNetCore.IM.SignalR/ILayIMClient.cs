using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.IM.SignalR
{
    public interface ILayIMClient
    {
        Task Receive(string messages);
    }
}
