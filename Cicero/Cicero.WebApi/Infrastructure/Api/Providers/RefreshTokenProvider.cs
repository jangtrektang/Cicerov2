using Cicero.Core.Helpers;
using Cicero.Core.Models;
using Cicero.Core.Repositories;
using Cicero.Persistence.Repositories;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cicero.WebApi.Infrastructure.Api.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly IAuthorizationRepository _authRepository;

        public RefreshTokenProvider()
        {
            _authRepository = new AuthorizationRepository();
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientId = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientId))
                return;

            var refreshTokenId = Guid.NewGuid().ToString("n");
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshToken()
            {
                Id = MembershipHelper.HashPassword(refreshTokenId),
                ClientId = clientId,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;
            token.ProtectedTicket = context.SerializeTicket();

            var result = await _authRepository.AddRefreshToken(token);

            if (result)
                context.SetToken(refreshTokenId);            
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var hashedTokenId = MembershipHelper.HashPassword(context.Token);

            var refreshToken = await _authRepository.FindRefreshToken(hashedTokenId);

            if(refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                var result = await _authRepository.RemoveRefreshToken(hashedTokenId);
            }
        }
    }
}