using System;
using Glyde.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Glyde.Di.AspNetDependencyInjection
{
    public class AspNetServiceProviderBootstrapperInitiator : ServiceProviderBootstrapperInitiator
    {

        protected override IServiceProviderConfigurator CreateServiceProviderConfigurator()
        {
            return new AspNetServiceProviderConfigurator(ServiceCollection);
        }

        internal override IServiceProvider CreateServiceProvider()
        {
            return ServiceCollection.BuildServiceProvider();
        }
    }
}