using System;
using Glyde.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Glyde.Di.AspNetDependencyInjection
{
    public class AspNetServiceProviderBootstrapperInitiator : ServiceProviderBootstrapperInitiator
    {

        protected override IServiceProviderConfigurationBuilder CreateConfigurationBuilder()
        {
            return new AspNetServiceProviderConfigurationBuilder(ServiceCollection);
        }
    }
}