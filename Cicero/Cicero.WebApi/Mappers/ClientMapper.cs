using Cicero.Core.Models;
using Cicero.WebApi.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cicero.WebApi.Mappers
{
    public class ClientMapper
    {
        public ClientMapper()
        {

        }

        public void Map(ClientWriteModel model, Client entity)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.ApplicationType = model.ApplicationType;
            entity.Active = model.Active;
            entity.AllowedOrigin = model.AllowedOrigin;
            entity.RefreshTokenLifeTime = model.RefreshTokenLifeTime;
            entity.Secret = model.Secret;
        }
    }
}