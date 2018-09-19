using LayIM.AspNetCore.Core.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.IM.SignalR.Auth
{
    public class UserAuthenticationHandler : AuthenticationHandler<UserAuthenticationOptions>
    {
        private readonly ILayIMUserFactory userFactory;

        public UserAuthenticationHandler(IOptionsMonitor<UserAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceProvider serviceProvider)
        : base(options, logger, encoder, clock)
        {
            userFactory = serviceProvider.GetService<ILayIMUserFactory>();
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var userId = userFactory.GetUserId(Request.HttpContext);
            if (string.IsNullOrEmpty(userId))
            {
                return Task.FromResult(AuthenticateResult.Fail("no user"));
            }
            var claims = new[] { new Claim("user", userId) };
            var identity = new ClaimsIdentity(claims, nameof(UserAuthenticationHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
