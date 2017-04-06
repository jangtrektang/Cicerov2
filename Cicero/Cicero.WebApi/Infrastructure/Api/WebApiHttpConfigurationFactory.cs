using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Web.Http.Controllers;
using Cicero.WebApi.Infrastructure.Api.Exceptions;
using log4net.Config;
using System.IO;
using System.Web.Http.ExceptionHandling;
using Autofac.Integration.WebApi;

namespace Cicero.WebApi.Infrastructure
{
    public static class WebApiHttpConfigurationFactory
    {
        public static HttpConfiguration Create(IContainer container)
        {
            HttpConfiguration configuration = new HttpConfiguration
            {
                // Set dependency resolver for ASP.NET WebApi framework
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };

            // Enable tracing
            EnableTracing(configuration);

            // Configure logging
            ConfigureLogging();

            RegisterRoutes(configuration);
            RegisterMediaTypeFormatters(configuration.Formatters);
            RegisterServices(configuration.Services);

            return configuration;
        }

        private static void EnableTracing(HttpConfiguration configuration)
        {
            var traceWriter = configuration.EnableSystemDiagnosticsTracing();
            traceWriter.IsVerbose = true;
            traceWriter.MinimumLevel = System.Web.Http.Tracing.TraceLevel.Debug;
        }

        private static void ConfigureLogging()
        {
            var directory = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);

            var configurationFile = new FileInfo($"{ directory.FullName }\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(configurationFile);
        }

        private static void RegisterRoutes(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
        }

        private static void RegisterMediaTypeFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.Clear();

            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            };

            settings.Converters.Add(new StringEnumConverter());

            formatters.Add(new JsonMediaTypeFormatter()
            {
                SerializerSettings = settings
            });
        }

        private static void RegisterServices(ServicesContainer services)
        {
            services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());
            services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}