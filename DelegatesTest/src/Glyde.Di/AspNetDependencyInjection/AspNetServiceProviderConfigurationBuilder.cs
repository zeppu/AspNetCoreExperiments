using Microsoft.Extensions.DependencyInjection;

namespace Glyde.Di.AspNetDependencyInjection
{
    public class AspNetServiceProviderConfigurationBuilder : IServiceProviderConfigurationBuilder
    {
        private readonly IServiceCollection _serviceCollection;

        public AspNetServiceProviderConfigurationBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void AddTransientService<TContract, TService>()
            where TService : class, TContract where TContract : class
        {
            _serviceCollection.AddTransient<TContract, TService>();
        }

        public void AddSingletonService<TContract, TService>()
            where TService : class, TContract where TContract : class
        {
            _serviceCollection.AddSingleton<TContract, TService>();
        }

        public void AddScopedService<TContract, TService>()
            where TService : class, TContract where TContract : class
        {
            _serviceCollection.AddScoped<TContract, TService>();
        }
    }
}