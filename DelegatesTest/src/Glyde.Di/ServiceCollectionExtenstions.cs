using System;
using System.Collections.Generic;
using System.Reflection;
using Glyde.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Glyde.Di
{
    public static class ServiceCollectionExtenstions
    {
        public static IServiceProvider RegisterAllServices<TServiceProviderBootstrapperInit>(this IServiceCollection serviceCollection,
            IEnumerable<Assembly> assemblies, IConfigurationProvider configurationProvider)
            where TServiceProviderBootstrapperInit : ServiceProviderBootstrapperInitiator, new()
        {
            var initiator = new TServiceProviderBootstrapperInit
            {
                ServiceCollection = serviceCollection,
                ConfigurationProvider = configurationProvider
            };
            initiator.Run(assemblies);

            return initiator.CreateServiceProvider();
        }
    }
}