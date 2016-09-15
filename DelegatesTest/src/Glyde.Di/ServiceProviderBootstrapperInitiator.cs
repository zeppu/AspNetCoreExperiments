using System;
using System.Collections.Generic;
using System.Reflection;
using Glyde.Bootstrapping;
using Glyde.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Glyde.Di
{
    public abstract class ServiceProviderBootstrapperInitiator : BootstrapInitiator<IServiceProviderBootstrapper>
    {
        internal IConfigurationProvider ConfigurationProvider { get; set; }

        internal IServiceCollection ServiceCollection { get; set; }

        protected abstract IServiceProviderConfigurator CreateServiceProviderConfigurator();

        public override void Run(IEnumerable<Assembly> assemblies)
        {
            var bootstrappers = GetBootstrappers(assemblies);
            var serviceProviderConfigurator = CreateServiceProviderConfigurator();

            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.RegisterServices(serviceProviderConfigurator, ConfigurationProvider);
            }
        }

        internal abstract IServiceProvider CreateServiceProvider();
    }
}