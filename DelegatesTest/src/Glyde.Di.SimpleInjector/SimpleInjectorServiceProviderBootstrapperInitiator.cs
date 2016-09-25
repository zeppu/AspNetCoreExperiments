using System;

namespace Glyde.Di.SimpleInjector
{
    public class SimpleInjectorServiceProviderBootstrapperInitiator : ServiceProviderBootstrapperInitiator
    {
        private readonly IServiceProviderConfigurationBuilder _configurationBuilder;

        public SimpleInjectorServiceProviderBootstrapperInitiator(IServiceProviderConfigurationBuilder configurationBuilder)
        {
            _configurationBuilder = configurationBuilder;
        }

        protected override IServiceProviderConfigurationBuilder CreateConfigurationBuilder()
        {
            return _configurationBuilder;
        }
    }
}