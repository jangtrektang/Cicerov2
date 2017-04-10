using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Factories
{
    public interface IRefreshTokenFactory
    {
        RefreshToken CreateRefreshToken(string id, string subject, string clientId, DateTime issuedUtc, DateTime expiresUtc, string protectedTicket);
    }
}
