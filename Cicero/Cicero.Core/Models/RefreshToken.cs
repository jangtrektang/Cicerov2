using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Models
{
    public class RefreshToken
    {
        public RefreshToken()
        {

        }

        public RefreshToken(string id, string subject, string clientId, DateTime issuedUtc, DateTime expiresUtc, string protectedTicket)
        {
            Id = id;
            Subject = subject;
            ClientId = clientId;
            IssuedUtc = issuedUtc;
            ExpiresUtc = expiresUtc;
            ProtectedTicket = protectedTicket;
        }

        public string Id { get; set; }
        public string Subject { get; set; }
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
    }
}
