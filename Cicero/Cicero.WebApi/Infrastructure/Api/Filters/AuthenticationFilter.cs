using Autofac.Integration.WebApi;
using Cicero.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Cicero.WebApi.Infrastructure.Api.Filters
{
    public class AuthenticationFilter : IAutofacAuthenticationFilter
    {
      
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null); 
        }
    }
}