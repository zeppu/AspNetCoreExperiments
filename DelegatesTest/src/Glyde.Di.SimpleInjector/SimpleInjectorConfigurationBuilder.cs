using SimpleInjector;

namespace Glyde.Di.SimpleInjector
{
    public class SimpleInjectorConfigurationBuilder : IServiceProviderConfigurationBuilder
    {
        private readonly Container _container;

        public SimpleInjectorConfigurationBuilder(Container container)
        {
            _container = container;
        }

        public void AddTransientService<TContract, TService>() where TContract : class where TService : class, TContract
        {
            throw new System.NotImplementedException();
        }

        public void AddSingletonService<TContract, TService>() where TContract : class where TService : class, TContract
        {
            throw new System.NotImplementedException();
        }

        public void AddScopedService<TContract, TService>() where TContract : class where TService : class, TContract
        {
            throw new System.NotImplementedException();
        }
    }
}