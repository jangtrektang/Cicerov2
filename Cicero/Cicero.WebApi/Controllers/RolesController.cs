using AutoMapper;
using Cicero.Common;
using Cicero.Core.Factories;
using Cicero.Core.Repositories;
using Cicero.WebApi.Infrastructure;
using Cicero.WebApi.Infrastructure.Extensions;
using Cicero.WebApi.Mappers;
using Cicero.WebApi.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cicero.WebApi.Controllers
{
    public class RolesController : WebApiController
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleFactory _roleFactory;
        private readonly RoleMapper _roleMapper;

        public RolesController(IRoleRepository roleRepository, IRoleFactory roleFactory)
        {
            if (roleRepository == null) throw new ArgumentNullException(nameof(roleRepository));
            if (roleFactory == null) throw new ArgumentNullException(nameof(roleFactory));

            _roleRepository = roleRepository;
            _roleFactory = roleFactory;

            _roleMapper = new RoleMapper();
        }

        [Route("roles")]
        [HttpGet]
        public IHttpActionResult Get(int? page = 1, int? pageSize = null, string sort = null)
        {
            var roles = _roleRepository.FindAll();

            if (string.IsNullOrEmpty(sort))
            {
                roles = roles.OrderBy(x => x.DisplayName);
            }
            else
            {
                // TODO: fix this
                //users = users.OrderBy(sort);
            }

            var pagedList = roles.ToPagedList(page ?? 1, pageSize ?? ApplicationSettings.DefaultPageSize);

            var models = pagedList.Select(Mapper.Map<RoleReadModel>);

            return Ok(models)
                .WithPaginationHeader(pagedList);
        }

        [Route("roles/{roleId}", Name = "GetRole")]
        [HttpGet]
        public IHttpActionResult GetRole(int roleId)
        {
            var role = _roleRepository.FindById(roleId);

            if (role == null)
                return NotFound();

            var model = Mapper.Map<RoleReadModel>(role);

            return Ok(model);
        }

        [Route("roles")]
        [HttpPost]
        public IHttpActionResult Post(RoleWriteModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CodeName))
                return BadRequest("No codename provided.");

            if (string.IsNullOrWhiteSpace(model.DisplayName))
                return BadRequest("No display name provided.");

            var role = _roleFactory.CreateRole(model.CodeName, model.DisplayName);

            _roleMapper.Map(model, role);

            _roleRepository.Save(role);

            var result = Mapper.Map<RoleReadModel>(role);

            return CreatedAtRoute("GetRole", new { roleId = role.Id }, result);
        }

        [Route("roles/{roleId}")]
        [HttpPut]
        public IHttpActionResult Put(int roleId, RoleWriteModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CodeName))
                return BadRequest("No codename provided.");

            if (string.IsNullOrWhiteSpace(model.DisplayName))
                return BadRequest("No display name provided.");

            var role = _roleRepository.FindById(roleId);

            if (role == null)
                return NotFound();

            _roleMapper.Map(model, role);

            _roleRepository.Save(role);

            return NoContent();
        }

        [Route("roles/{roleId}")]
        [HttpDelete]
        public IHttpActionResult Delete(int userId)
        {
            return NoContent();
        }
    }
}