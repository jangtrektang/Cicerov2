using Cicero.Core.Enums;
using Cicero.Core.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly ILoggingService _loggingService;

        public SecurityService(ILoggingService loggingService)
        {
            if(loggingService == null) throw new ArgumentNullException(nameof(loggingService));

            _loggingService = loggingService;
        }

        public bool AuthorizeActivity(Activity activity)
        {
            var user = System.Threading.Thread.CurrentPrincipal.Identity as ClaimsIdentity;

            if(user == null)
            {
                _loggingService.Error("No CurrentPrincipal found on thread");
                return false;
            }

            _loggingService.Debug($"Authorizing activity '{activity}' for user '{user.Name}'");

            var allowed = false;

            // TODO: Check if ucrrent user is authorized to perform the requested activity

            if(!allowed)
            {
                _loggingService.Warn("'User '{0}' is not authorized for activity '{1}'", user.Name, activity.ToString());
            }

            return allowed;
        }
    }
}
