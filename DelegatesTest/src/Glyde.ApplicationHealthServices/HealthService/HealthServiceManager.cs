using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationHealthServices.HealthService
{
    internal class HealthServiceManager : IDisposable
    {
        private readonly List<IHealthService> _services;
        private readonly List<IBackgroundHealthService> _backgroundServices;

        public HealthServiceManager(IServiceProvider serviceProvider)
        {
            // activate all services
            _services = serviceProvider.GetServices<IHealthService>().ToList();
            _backgroundServices = _services.OfType<IBackgroundHealthService>().ToList();
            _backgroundServices.ForEach( s => s.Start());
        }

        public IEnumerable<IHealthService> GetHealthServices()
        {
            return _services;
        }

        public IEnumerable<ServiceHealthStatus> GetHealthServicesStatus()
        {
            return _services
                .Select(x => x.GetServiceStatus())
                .ToList();
        }

        public void Dispose()
        {
            _backgroundServices.ForEach( s => s.Stop() );
        }
    }
}