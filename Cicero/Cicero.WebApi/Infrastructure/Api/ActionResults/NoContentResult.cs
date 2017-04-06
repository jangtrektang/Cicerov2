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
    public class NoContentResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;

        public NoContentResult(HttpRequestMessage request)
        {
            _request = request;
        }

        public NoContentResult(ApiController controller)
            : this(controller.Request)
        {

        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(
                _request.CreateResponse(System.Net.HttpStatusCode.Forbidden));
        }
    }
}