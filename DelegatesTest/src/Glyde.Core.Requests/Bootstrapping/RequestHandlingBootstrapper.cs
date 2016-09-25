using Glyde.Configuration;
using Glyde.Di;
using Microsoft.AspNetCore.Http;

namespace Glyde.Core.Requests.Bootstrapping
{
    public class RequestHandlingBootstrapper : IServiceProviderBootstrapper
    {
        public void RegisterServices(IServiceProviderConfigurationBuilder serviceProviderConfigurationBuilder,
            IConfigurationProvider configurationProvider)
        {
            serviceProviderConfigurationBuilder.AddScopedService<IRequestContext, RequestContextWrapper>();
        }
    }
}