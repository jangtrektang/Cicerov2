using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Cicero.WebApi.Infrastructure.Autofac;
using Autofac;
using Cicero.WebApi.Infrastructure;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Cors;
using Microsoft.Owin.Cors;
using Cicero.Common;
using AutoMapper;
using Cicero.WebApi.Infrastructure.Api;

[assembly: OwinStartup(typeof(Cicero.WebApi.Startup))]

namespace Cicero.WebApi
{
    public class Startup
    {
        private static readonly IContainer Container = ContainerFactory.CreateContainer<ApplicationModule>();
        private static readonly HttpConfiguration HttpConfiguration = WebApiHttpConfigurationFactory.Create(Container);

        public Startup()
        {
            ConfigureJsonConvert();
            ConfigureAutoMapper();
            ConfigureServiceLocator();
        }

        #region Non-Owin / ApiController Configuration

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(x => x.AddProfile(new AutoMapperMappingProfile()));
            Mapper.AssertConfigurationIsValid();
        }

        private void ConfigureJsonConvert()
        {
            JsonConvert.DefaultSettings = () => HttpConfiguration.Formatters.JsonFormatter.SerializerSettings;
        }

        private void ConfigureServiceLocator()
        {

        }

        #endregion

        #region OWIN configuration

        public void Configuration(IAppBuilder app)
        {
            ConfigureCors(app);

            app.UseAutofacMiddleware(Container);
            app.UseAutofacWebApi(HttpConfiguration);
            app.UseWebApi(HttpConfiguration);
        }

        public void ConfigureCors(IAppBuilder app)
        {
            var allowedOrigins = ApplicationSettings.AllowedOrigins.Split(';');

            var policy = new CorsPolicy()
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                AllowAnyOrigin = true,
                SupportsCredentials = true,
                ExposedHeaders = { "X-Meta", "X-Pagination", "X-HTTP-StatusCode-Transform" },
                PreflightMaxAge = 60 * 10,
            };

            foreach(var origin in allowedOrigins)
            {
                policy.Origins.Add(origin);
            }

            app.UseCors(new CorsOptions()
            {
                PolicyProvider = new CorsPolicyProvider()
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            });
        }

        #endregion

    }
}
