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
    }
}