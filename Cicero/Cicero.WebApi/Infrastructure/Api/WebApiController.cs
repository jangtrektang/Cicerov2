using Cicero.WebApi.Infrastructure.Api.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cicero.WebApi.Infrastructure
{
    public class WebApiController : ApiController
    {
        #region Action results

        protected NoContentResult NoContent()
        {
            return new NoContentResult(this);
        }

        protected ForbiddenResult Forbidden()
        {
            return new ForbiddenResult(this);
        }

        protected UnsupportedMediaTypeResult UnsupportedMediaType()
        {
            return new UnsupportedMediaTypeResult(this);
        }

        #endregion
    }
}