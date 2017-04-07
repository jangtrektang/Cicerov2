using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Cicero.Core;
using Cicero.Persistence;
using Cicero.WebApi.Infrastructure.Api;
using Cicero.WebApi.Infrastructure.Api.Filters;
using Module = Autofac.Module;
using Cicero.WebApi.Infrastructure.Api.Providers;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using System;

namespace Cicero.WebApi.Infrastructure.Autofac
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterWebApiComponents(builder);
            RegisterCoreComponents(builder);
            RegisterFilters(builder);
            RegisterDomainEventHandlers(builder);
            RegisterCommonComponents(builder);
        }

        private static void RegisterWebApiComponents(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .InstancePerRequest();

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
        }

        private static void RegisterCoreComponents(ContainerBuilder builder)
        {
            // Register the database context
            builder.Register<Cicerov2Context>(x => new Cicerov2Context())
                .InstancePerRequest();

            // Register all repositories in the persistence object
            builder.RegisterAssemblyTypes(typeof(PersistenceAssembly).Assembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Register all services in the core project
            builder.RegisterAssemblyTypes(typeof(CoreAssembly).Assembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Register all factories in the core project
            builder.RegisterAssemblyTypes(typeof(CoreAssembly).Assembly)
                .Where(x => x.Name.EndsWith("Factory"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Register Authorizationprovider
            builder.RegisterType<AuthorizationProvider>()
                .AsSelf()
                .SingleInstance();
        }

        private static void RegisterCommonComponents(ContainerBuilder builder)
        {

        }

        private static void RegisterFilters(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticationFilter>()
                .AsWebApiAuthenticationFilterFor<WebApiController>()
                .InstancePerRequest();

            builder.RegisterType<LoggingFilter>()
                .AsWebApiActionFilterFor<WebApiController>()
                .InstancePerRequest();
        }

        private static void RegisterDomainEventHandlers(ContainerBuilder builder)
        {

        }  
    }
}