using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApplicationHealthServices.HealthService;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationHealthServices
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationHealthServices(this IServiceCollection services)
        {
            services.AddSingleton<HealthServiceManager>();
            services.AddSingleton<ApplicationHealthRouter>();
        }

        public static void AddApplicationHealthServices(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<HealthServiceManager>();
            services.AddSingleton<ApplicationHealthRouter>();

            var healthServices = assemblies.SelectMany(a => a.GetTypes())
                .Select(t => t.GetTypeInfo())
                .Where(t => t.IsClass & !t.IsAbstract & t.ImplementedInterfaces.Contains(typeof(IHealthService)))
                .ToList();

            healthServices.ForEach(info => services.AddSingleton(typeof(IHealthService), info.AsType()));

        }
    }
}