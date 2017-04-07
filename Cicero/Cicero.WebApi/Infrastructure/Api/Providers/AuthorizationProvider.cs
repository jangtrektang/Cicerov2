using Autofac;
using Cicero.Core.Helpers;
using Cicero.Core.Models;
using Cicero.Core.Repositories;
using Cicero.Persistence.Repositories;
using Cicero.WebApi.Infrastructure.Autofac;
using Microsoft.Owin.Security;
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
        private readonly IAuthorizationRepository _authRepository;

        public AuthorizationProvider()
        {
            _userRepository = new UserRepository();
            _authRepository = new AuthorizationRepository();
        }
        public AuthorizationProvider(IUserRepository userRepository, IAuthorizationRepository authRepository)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (authRepository == null) throw new ArgumentNullException(nameof(authRepository));

            _userRepository = userRepository;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var clientId = string.Empty;
            var clientSecret = string.Empty;
            Client client = null;

            if(!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if(context.ClientId == null)
            {
                context.Validated();

                return Task.FromResult<object>(null);
            }

            client = _authRepository.FindClient(context.ClientId);

            if(client == null)
            {
                context.SetError("invalid_clientId", string.Format("Cliennt {0} is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if(client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if(string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if(client.Secret != MembershipHelper.HashPassword(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid");
                    }
                }
            }

            if(!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());
            
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allowed-Origin", new[] { allowedOrigin });

            var user = await _userRepository.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                },
                {
                    "userName", context.UserName
                }
            });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach(KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);

            return Task.FromResult<object>(null);
        }
    }
}
