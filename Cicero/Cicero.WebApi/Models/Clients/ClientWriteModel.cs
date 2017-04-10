using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cicero.WebApi.Models.Clients
{
    public class ClientWriteModel
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}