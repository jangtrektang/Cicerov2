using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Models;

namespace Cicero.Core.Factories
{
    public class ClientFactory : IClientFactory
    {
        public Client CreateClient(string id, string secret, string name, ApplicationTypes applicationType, bool active, int refreshTokenLifeTime, string allowedOrigin)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));

            if (string.IsNullOrEmpty(secret))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(secret));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            if (string.IsNullOrEmpty(allowedOrigin))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(allowedOrigin));

            var entity = new Client(id, secret, name, applicationType, active, refreshTokenLifeTime, allowedOrigin);

            return entity;
        }
    }
}
