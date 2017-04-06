using AutoMapper;
using Cicero.Core.Models;
using Cicero.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cicero.WebApi.Infrastructure.Api
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<User, UserReadModel>();
            CreateMap<Role, RoleReadModel>();
        }
    }
}