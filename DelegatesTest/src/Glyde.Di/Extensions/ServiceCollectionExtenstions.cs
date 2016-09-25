using System;
using System.Collections.Generic;
using System.Reflection;
using Glyde.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Glyde.Di
{
    public static class ServiceCollectionExtenstions
    {
        public static void RegisterAllServices(this IServiceCollection serviceCollection, DependencyInjectionBootstrapper dependencyInjection,
            IEnumerable<Assembly> assemblies, IConfigurationProvider configurationProvider)

        {
            var initiator = dependencyInjection.CreateServiceProviderBootstrapperInitiator(serviceCollection, configurationProvider);
            initiator.Run(assemblies);
        }
    }
}