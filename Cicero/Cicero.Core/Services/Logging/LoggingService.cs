using Cicero.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Services.Logging
{
    public class LoggingService : ILoggingService
    {
        private ILog GetLogger(string name = "")
        {
            if (string.IsNullOrWhiteSpace(name))
                name = "Default";

            return LogManager.GetLogger(name);
        }

        public void TraceRequest(Uri url, Uri urlReferrer, string httpMethod, string userHostAddress, string queryString, string formData)
        {
            ThreadContext.Properties[Constants.Logging.URL] = $"{url.Scheme}://{url.Host}{url.AbsolutePath}";
            ThreadContext.Properties[Constants.Logging.URLReferrer] = urlReferrer != null ? urlReferrer.AbsoluteUri : "";
            ThreadContext.Properties[Constants.Logging.RequestType] = httpMethod;
            ThreadContext.Properties[Constants.Logging.HostAddress] = userHostAddress;
            ThreadContext.Properties[Constants.Logging.QueryString] = queryString;
            ThreadContext.Properties[Constants.Logging.Content] = formData;

            GetLogger("RequestTracer").Debug("");
        }

        private void SetCalleeInfoOnContext(string memberName, string sourceFilePath, int sourceLineNumber)
        {
            var callee = $"'{memberName}' in {sourceFilePath}:{sourceLineNumber}";

            ThreadContext.Properties[Constants.Logging.Caller] = callee;
        }

        public void Debug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            SetCalleeInfoOnContext(memberName, sourceFilePath, sourceLineNumber);
            GetLogger().Debug(message);
        }

        public void Info(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            SetCalleeInfoOnContext(memberName, sourceFilePath, sourceLineNumber);
            GetLogger().Info(message);
        }

        public void Warn(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            SetCalleeInfoOnContext(memberName, sourceFilePath, sourceLineNumber);
            GetLogger().Warn(message);
        }

        public void Error(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            SetCalleeInfoOnContext(memberName, sourceFilePath, sourceLineNumber);
            GetLogger().Error(message);
        }

        public void Error(Exception e, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            SetCalleeInfoOnContext(memberName, sourceFilePath, sourceLineNumber);
            GetLogger().Error("An exception has occurred", e);
        }

        public void Fatal(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            SetCalleeInfoOnContext(memberName, sourceFilePath, sourceLineNumber);
            GetLogger().Fatal(message);
        }

        public void Fatal(Exception e, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            SetCalleeInfoOnContext(memberName, sourceFilePath, sourceLineNumber);
            GetLogger().Info("A fatal exception has occurred", e);
        }                
    }
}
