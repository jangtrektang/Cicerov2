using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Models;

namespace Cicero.Core.Factories
{
    public class RefreshTokenFactory : IRefreshTokenFactory
    {
        public RefreshToken CreateRefreshToken(string id, string subject, string clientId, DateTime issuedUtc, DateTime expiresUtc, string protectedTicket)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(subject));

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(clientId));

            if (string.IsNullOrEmpty(protectedTicket))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(protectedTicket));

            var entity = new RefreshToken(id, subject, clientId, issuedUtc, expiresUtc, protectedTicket);

            return entity;
        }
    }
}
