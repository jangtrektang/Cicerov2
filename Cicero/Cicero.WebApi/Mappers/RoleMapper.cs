using Cicero.Core.Models;
using Cicero.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cicero.WebApi.Mappers
{
    public class RoleMapper
    {
        public RoleMapper()
        {

        }

        public void Map(RoleWriteModel model, Role entity)
        {
            entity.CodeName = model.CodeName;
            entity.DisplayName = model.DisplayName;
        }
    }
}