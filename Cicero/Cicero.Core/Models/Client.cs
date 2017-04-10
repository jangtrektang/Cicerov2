using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Models
{
    public class Client
    {
        public Client()
        {

        }

        public Client(string id, string secret, string name, ApplicationTypes applicationType, bool active, int refreshTokenLifeTime, string allowedOrigin)
        {
            Id = id;
            Secret = secret;
            Name = name;
            ApplicationType = applicationType;
            Active = active;
            RefreshTokenLifeTime = refreshTokenLifeTime;
            AllowedOrigin = allowedOrigin;
        }

        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}
