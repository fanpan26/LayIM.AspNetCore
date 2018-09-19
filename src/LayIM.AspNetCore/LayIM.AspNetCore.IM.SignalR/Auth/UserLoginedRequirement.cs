using LayIM.AspNetCore.Core.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.IM.SignalR.Auth
{
    public class UserLoginedRequirement : IAuthorizationRequirement
    {
        private readonly ILayIMUserFactory userFactory;
        public string GetUserId()
        {
            return userFactory.GetUserId(null);
        }
        public UserLoginedRequirement()
        {
            userFactory = LayIMServiceLocator.GetService<ILayIMUserFactory>();
        }
    }
}
