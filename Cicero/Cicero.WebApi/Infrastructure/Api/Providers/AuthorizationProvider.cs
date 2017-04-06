using Cicero.Core.Repositories;
using Cicero.Persistence.Repositories;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Cicero.WebApi.Infrastructure.Api.Providers
{
    public class AuthorizationProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserRepository _userRepository;

        public AuthorizationProvider(IUserRepository userRepository)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));

            _userRepository = userRepository;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allowed-Origin", new[] { "*" });

            var user = _userRepository.FindUser(context.UserName, context.Password);

            if(user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            identity.AddClaim(new Claim("sub", context.UserName));

            context.Validated(identity);
        }       
    }
}