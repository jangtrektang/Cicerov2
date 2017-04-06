using System.Configuration;

namespace Cicero.Common
{
    public class ApplicationSettings
    {
        private class Keys
        {
            public const string AllowedOrigins = "AllowedOrigins";
            public const string DefaultPageSize = "DefaultPageSize";
        }

        public static string AllowedOrigins => ConfigurationManager.AppSettings[Keys.AllowedOrigins];

        public static int DefaultPageSize
        {
            get
            {
                int result = 10;
                int.TryParse(ConfigurationManager.AppSettings[Keys.DefaultPageSize], out result);
                return result;
            }
        }
    }
}
