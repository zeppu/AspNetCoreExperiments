using Glyde.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Glyde.Di.AspNetDependencyInjection
{
    public class AspNetCoreBuiltInDependencyInjection : DependencyInjectionBootstrapper
    {
        public override void RegisterWithApplication(IApplicationBuilder applicationBuilder)
        {

        }

        public override ServiceProviderBootstrapperInitiator CreateServiceProviderBootstrapperInitiator(IServiceCollection serviceCollection,
            IConfigurationProvider configurationProvider)
        {
            return new AspNetServiceProviderBootstrapperInitiator()
            {
                ConfigurationProvider = configurationProvider,
                ServiceCollection = serviceCollection
            };
        }
    }
}