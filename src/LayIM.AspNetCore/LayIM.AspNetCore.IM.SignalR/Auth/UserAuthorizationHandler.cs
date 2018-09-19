using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.IM.SignalR.Auth
{
    public class UserAuthorizationHandler : AuthorizationHandler<UserLoginedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserLoginedRequirement requirement)
        {
            string userId = requirement.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
