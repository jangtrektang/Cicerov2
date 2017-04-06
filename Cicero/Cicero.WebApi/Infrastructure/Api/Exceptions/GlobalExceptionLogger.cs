using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Cicero.WebApi.Infrastructure.Api.Exceptions
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            //var loggingService = context.Request.GetOwinContext().GetAutofacLifetimeScope().Resolve<ILoggingService>();
            //loggingService.Error(context.Exception);
        }
    }

}