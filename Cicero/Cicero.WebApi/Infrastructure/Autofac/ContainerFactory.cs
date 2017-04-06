using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cicero.WebApi.Infrastructure.Autofac
{
    public static class ContainerFactory
    {
        public static IContainer CreateContainer<TModule>() where TModule : Module, new()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<TModule>();

            return builder.Build();
        }
    }
}