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
    public class ResponseDecoratingResult : IHttpActionResult
    {
        private readonly IHttpActionResult _result;
        private readonly Action<HttpResponseMessage> _action;

        public ResponseDecoratingResult(IHttpActionResult result, Action<HttpResponseMessage> action)
        {
            _result = result;
            _action = action;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return _result.ExecuteAsync(cancellationToken)
                .ContinueWith(action =>
                {
                    _action(action.Result);
                    return action.Result;
                }, cancellationToken);
        }
    }
}