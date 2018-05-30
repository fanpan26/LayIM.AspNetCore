using LayIM.AspNetCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Core.IM
{

    public interface ILayIMServer
    {
        Task<TokenResult> GetToken(string userId);
        LayIMBaseResult SendPrivateMessage(string targetId, object message);
        LayIMBaseResult SendGroupMessage(string targetId, object message);
        LayIMBaseResult SendSystemMessage(string targetId, object message);
    }
}
