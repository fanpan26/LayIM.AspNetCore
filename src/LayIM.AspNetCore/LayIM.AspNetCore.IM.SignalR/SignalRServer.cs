using LayIM.AspNetCore.Core.IM;
using LayIM.AspNetCore.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.IM.SignalR
{
    public class SignalRServer : ILayIMServer
    {
        public Task<TokenResult> GetToken(string userId)
          => Task.FromResult(new TokenResult
            {
                token = JwtTokenProvider.GetToken(userId)
            });
        

        public LayIMBaseResult SendGroupMessage(string targetId, object message)
        {
            throw new NotImplementedException();
        }

        public LayIMBaseResult SendPrivateMessage(string targetId, object message)
        {
            throw new NotImplementedException();
        }

        public LayIMBaseResult SendSystemMessage(string targetId, object message)
        {
            throw new NotImplementedException();
        }
    }
}
