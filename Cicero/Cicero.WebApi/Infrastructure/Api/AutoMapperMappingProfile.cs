using AutoMapper;
using Cicero.Core.Models;
using Cicero.WebApi.Models;
using Cicero.WebApi.Models.Clients;
using Cicero.WebApi.Models.RefreshTokens;
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
            CreateMap<Client, ClientReadModel>();
            CreateMap<RefreshToken, RefreshTokenReadModel>();
        }
    }
}