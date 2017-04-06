using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Cicero.WebApi.Infrastructure.Api.Exceptions
{
    public class GlobalExceptionHandler : System.Web.Http.ExceptionHandling.ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            //var loggingService = context.Request.GetOwinContext().GetAutofacLifetimeScope().Resolve<ILoggingService>();
            //loggingService.Error(context.Exception);
        }
    }
}