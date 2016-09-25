using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glyde.Bootstrapping;
using Glyde.Configuration;

namespace Glyde.Di
{
    public interface IServiceProviderBootstrapper : IBootstrapper
    {
        void RegisterServices(IServiceProviderConfigurationBuilder serviceProviderConfigurationBuilder, IConfigurationProvider configurationProvider );
    }
}
