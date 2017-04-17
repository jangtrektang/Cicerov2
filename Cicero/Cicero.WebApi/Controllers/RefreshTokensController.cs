using AutoMapper;
using Cicero.Common;
using Cicero.Core.Factories;
using Cicero.Core.Repositories;
using Cicero.WebApi.Infrastructure;
using Cicero.WebApi.Infrastructure.Extensions;
using Cicero.WebApi.Mappers;
using Cicero.WebApi.Models;
using Cicero.WebApi.Models.Clients;
using Cicero.WebApi.Models.RefreshTokens;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cicero.WebApi.Controllers
{
    public class RefreshTokensController : WebApiController
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRefreshTokenFactory _refreshTokenFactory;
        private readonly RefreshTokenMapper _refreshTokenMapper;

        public RefreshTokensController(IRefreshTokenRepository refreshTokenRepository, IRefreshTokenFactory refreshTokenFactory)
        {
            if (refreshTokenRepository == null) throw new ArgumentNullException(nameof(refreshTokenRepository));
            if (refreshTokenFactory == null) throw new ArgumentNullException(nameof(refreshTokenFactory));

            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenFactory = refreshTokenFactory;

            _refreshTokenMapper = new RefreshTokenMapper();
        }

        [Route("refreshtokens")]
        [HttpGet]
        public IHttpActionResult Get(int? page = 1, int? pageSize = null, string sort = null)
        {
            var refreshTokens = _refreshTokenRepository.FindAll();

            if (string.IsNullOrEmpty(sort))
            {
                refreshTokens = refreshTokens.OrderBy(x => x.Id);
            }
            else
            {
                // TODO: fix this
                //users = users.OrderBy(sort);
            }

            var pagedList = refreshTokens.ToPagedList(page ?? 1, pageSize ?? ApplicationSettings.DefaultPageSize);

            var models = pagedList.Select(Mapper.Map<RefreshTokenReadModel>);

            return Ok(models)
                .WithPaginationHeader(pagedList);
        }

        [Route("refreshtokens/{refreshTokenId}", Name = "GetRefreshToken")]
        [HttpGet]
        public IHttpActionResult GetRefreshToken(string refreshTokenId)
        {
            var refreshToken = _refreshTokenRepository.FindById(refreshTokenId);

            if (refreshToken == null)
                return NotFound();

            var model = Mapper.Map<RefreshTokenReadModel>(refreshToken);

            return Ok(model);
        }

        [Route("refreshTokens")]
        [HttpPost]
        public IHttpActionResult Post(RefreshTokenWriteModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                return BadRequest("No id provided.");

            if (string.IsNullOrWhiteSpace(model.Subject))
                return BadRequest("No subject provided.");

            if (string.IsNullOrWhiteSpace(model.ClientId))
                return BadRequest("No clientId provided.");

            if (string.IsNullOrWhiteSpace(model.ProtectedTicket))
                return BadRequest("No protected ticket provided.");

            var refreshToken = _refreshTokenFactory.CreateRefreshToken(model.Id, model.Subject, model.ClientId, model.IssuedUtc, model.ExpiresUtc, model.ProtectedTicket);

            _refreshTokenMapper.Map(model, refreshToken);

            _refreshTokenRepository.Save(refreshToken);

            var result = Mapper.Map<RefreshTokenReadModel>(refreshToken);

            return CreatedAtRoute("GetrefreshToken", new { refreshTokenId = refreshToken.Id }, result);
        }

        [Route("refreshTokens/{refreshTokenId}")]
        [HttpPut]
        public IHttpActionResult Put(string refreshTokenId, RefreshTokenWriteModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                return BadRequest("No id provided.");

            if (string.IsNullOrWhiteSpace(model.Subject))
                return BadRequest("No subject provided.");

            if (string.IsNullOrWhiteSpace(model.ClientId))
                return BadRequest("No clientId provided.");

            if (string.IsNullOrWhiteSpace(model.ProtectedTicket))
                return BadRequest("No protected ticket provided.");

            var refreshToken = _refreshTokenRepository.FindById(refreshTokenId);

            if (refreshToken == null)
                return NotFound();

            _refreshTokenMapper.Map(model, refreshToken);

            _refreshTokenRepository.Save(refreshToken);

            return NoContent();
        }

        [Route("refreshTokens/{refreshTokenId}")]
        [HttpDelete]
        public IHttpActionResult Delete(string refreshTokenId)
        {
            return NoContent();
        }
    }
}