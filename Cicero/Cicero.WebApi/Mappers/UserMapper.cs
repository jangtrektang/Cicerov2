using Cicero.Core.Models;
using Cicero.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cicero.WebApi.Mappers
{
    public class UserMapper
    {
        public UserMapper()
        {

        }

        public void Map(UserWriteModel model, User entity)
        {
            entity.UserName = model.UserName;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.Password = model.Password;
        }
    }
}