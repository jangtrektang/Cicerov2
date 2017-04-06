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
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cicero.WebApi.Controllers
{
    public class UsersController : WebApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly UserMapper _userMapper;
        
        public UsersController(IUserRepository userRepository, IUserFactory userFactory)
        {
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (userFactory == null) throw new ArgumentNullException(nameof(userFactory));

            _userRepository = userRepository;
            _userFactory = userFactory;

            _userMapper = new UserMapper();
        }

        [Route("users")]
        [HttpGet]
        public IHttpActionResult Get(int? page = 1, int? pageSize = null, string sort = null)
        {
            var users = _userRepository.FindAll();

            if(string.IsNullOrEmpty(sort))
            {
                users = users.OrderBy(x => x.UserName);
            }
            else
            {
                // TODO: fix this
                //users = users.OrderBy(sort);
            }

            var pagedList = users.ToPagedList(page ?? 1, pageSize ?? ApplicationSettings.DefaultPageSize);

            var models = pagedList.Select(Mapper.Map<UserReadModel>);

            return Ok(models)
                .WithPaginationHeader(pagedList);
        }

        [Route("users/userId", Name = "GetUser")]
        [HttpGet]
        public IHttpActionResult GetUser(int userId)
        {
            var user = _userRepository.FindById(userId);

            if (user == null)
                return NotFound();

            var model = Mapper.Map<UserReadModel>(user);

            return Ok(model);
        }

        [Route("users")]
        [HttpPost]
        public IHttpActionResult Post(UserWriteModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName))
                return BadRequest("No username provided.");

            if (string.IsNullOrWhiteSpace(model.Email))
                return BadRequest("No email provided.");

            var user = _userFactory.CreateUser(model.UserName, model.FirstName, model.LastName, model.Email, model.Password);

            _userMapper.Map(model, user);

            _userRepository.Save(user);

            var result = Mapper.Map<UserReadModel>(user);

            return CreatedAtRoute("GetUser", new { userId = user.Id }, result);
        }

        [Route("users/{userId}")]
        [HttpPut]
        public IHttpActionResult Put(int userId, UserWriteModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName))
                return BadRequest("No username provided.");

            if (string.IsNullOrWhiteSpace(model.Email))
                return BadRequest("No email provided.");

            var user = _userRepository.FindById(userId);

            if (user == null)
                return NotFound();

            _userMapper.Map(model, user);

            _userRepository.Save(user);

            return NoContent();
        }

        [Route("users/{userId}")]
        [HttpDelete]
        public IHttpActionResult Delete(int userId)
        {
            return NoContent();
        }

    }
}
