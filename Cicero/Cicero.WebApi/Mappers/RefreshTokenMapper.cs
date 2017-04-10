using Cicero.Core.Models;
using Cicero.WebApi.Models.RefreshTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cicero.WebApi.Mappers
{
    public class RefreshTokenMapper
    {
        public RefreshTokenMapper()
        {

        }

        public void Map(RefreshTokenWriteModel model, RefreshToken entity)
        {
            entity.Id = model.Id;
            entity.Subject = model.Subject;
            entity.ProtectedTicket = model.ProtectedTicket;
            entity.IssuedUtc = model.IssuedUtc;
            entity.ExpiresUtc = model.ExpiresUtc;
            entity.ClientId = model.ClientId;
        }
    }
}