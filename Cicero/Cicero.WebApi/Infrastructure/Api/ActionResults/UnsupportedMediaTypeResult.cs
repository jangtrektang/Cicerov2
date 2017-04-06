using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Cicero.WebApi.Infrastructure.Api.ActionResults
{
    public class UnsupportedMediaTypeResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;

        public UnsupportedMediaTypeResult(HttpRequestMessage request)
        {
            _request = request;
        }

        public UnsupportedMediaTypeResult(ApiController controller) : this(controller.Request)
        {

        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _request.CreateResponse(System.Net.HttpStatusCode.UnsupportedMediaType));
        }
    }
}