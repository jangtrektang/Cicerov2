using AutoMapper;
using Cicero.Common;
using Cicero.Core.Factories;
using Cicero.Core.Repositories;
using Cicero.WebApi.Infrastructure;
using Cicero.WebApi.Infrastructure.Extensions;
using Cicero.WebApi.Mappers;
using Cicero.WebApi.Models;
using Cicero.WebApi.Models.Clients;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cicero.WebApi.Controllers
{
    public class ClientsController : WebApiController
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientFactory _clientFactory;
        private readonly ClientMapper _clientMapper;

        public ClientsController(IClientRepository clientRepository, IClientFactory clientFactory)
        {
            if (clientRepository == null) throw new ArgumentNullException(nameof(clientRepository));
            if (clientFactory == null) throw new ArgumentNullException(nameof(clientFactory));

            _clientRepository = clientRepository;
            _clientFactory = clientFactory;

            _clientMapper = new ClientMapper();
        }

        [Route("clients")]
        [HttpGet]
        public IHttpActionResult Get(int? page = 1, int? pageSize = null, string sort = null)
        {
            var clients = _clientRepository.FindAll();

            if (string.IsNullOrEmpty(sort))
            {
                clients = clients.OrderBy(x => x.Id);
            }
            else
            {
                // TODO: fix this
                //users = users.OrderBy(sort);
            }

            var pagedList = clients.ToPagedList(page ?? 1, pageSize ?? ApplicationSettings.DefaultPageSize);

            var models = pagedList.Select(Mapper.Map<ClientReadModel>);

            return Ok(models)
                .WithPaginationHeader(pagedList);
        }

        [Route("clients/clientId", Name = "GetClient")]
        [HttpGet]
        public IHttpActionResult GetRole(string clientId)
        {
            var client = _clientRepository.FindById(clientId);

            if (client == null)
                return NotFound();

            var model = Mapper.Map<ClientReadModel>(client);

            return Ok(model);
        }

        [Route("clients")]
        [HttpPost]
        public IHttpActionResult Post(ClientWriteModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                return BadRequest("No id provided.");

            if (string.IsNullOrWhiteSpace(model.Secret))
                return BadRequest("No secret provided.");

            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("No name provided.");

            if (string.IsNullOrWhiteSpace(model.AllowedOrigin))
                return BadRequest("No allowed origin provided.");

            var client = _clientFactory.CreateClient(model.Id, model.Secret, model.Name, model.ApplicationType, model.Active, model.RefreshTokenLifeTime, model.AllowedOrigin);

            _clientMapper.Map(model, client);

            _clientRepository.Save(client);

            var result = Mapper.Map<ClientReadModel>(client);

            return CreatedAtRoute("GetClient", new { clientId = client.Id }, result);
        }

        [Route("clients/{clientId}")]
        [HttpPut]
        public IHttpActionResult Put(string clientId, ClientWriteModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                return BadRequest("No id provided.");

            if (string.IsNullOrWhiteSpace(model.Secret))
                return BadRequest("No secret provided.");

            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("No name provided.");

            if (string.IsNullOrWhiteSpace(model.AllowedOrigin))
                return BadRequest("No allowed origin provided.");

            var client = _clientRepository.FindById(clientId);

            if (client == null)
                return NotFound();

            _clientMapper.Map(model, client);

            _clientRepository.Save(client);

            return NoContent();
        }

        [Route("clients/{clientId}")]
        [HttpDelete]
        public IHttpActionResult Delete(string clientId)
        {
            return NoContent();
        }
    }
}