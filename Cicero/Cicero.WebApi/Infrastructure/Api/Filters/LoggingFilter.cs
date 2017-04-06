using Autofac.Integration.WebApi;
using Cicero.Core.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace Cicero.WebApi.Infrastructure.Api.Filters
{
    public class LoggingFilter : IAutofacActionFilter
    {
        private readonly ILoggingService _loggingService;

        public LoggingFilter(ILoggingService loggingService)
        {
            if (loggingService == null) throw new ArgumentNullException(nameof(loggingService));
            _loggingService = loggingService;
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            throw new NotImplementedException();
        }

        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            throw new NotImplementedException();
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var request = actionContext.Request;

            string requestBody = string.Empty;

            actionContext.Request.Content.LoadIntoBufferAsync().Wait();

            // No need to load the request content if its a file upload
            if(actionContext.Request.Content.IsMimeMultipartContent())
            {
                requestBody = "FILE DATA";
            }
            else
            {
                requestBody = actionContext.Request.Content.ReadAsStringAsync().Result;
            }

            _loggingService.TraceRequest(request.RequestUri,
                request.Headers.Referrer, request.Method.Method,
                actionContext.Request.GetOwinContext()?.Request?.RemoteIpAddress ?? "",
                string.Join("&", request.GetQueryNameValuePairs().Select(x => $"{x.Key}={x.Value}")),
                requestBody);

            return Task.FromResult<object>(null);
        }
    }
}