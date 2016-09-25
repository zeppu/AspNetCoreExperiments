using System;
using Glyde.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Glyde.Di
{
    public abstract class DependencyInjectionBootstrapper
    {
        public abstract void RegisterWithApplication(IApplicationBuilder applicationBuilder);

        public abstract ServiceProviderBootstrapperInitiator CreateServiceProviderBootstrapperInitiator(IServiceCollection serviceCollection, IConfigurationProvider configurationProvider);
    }
}