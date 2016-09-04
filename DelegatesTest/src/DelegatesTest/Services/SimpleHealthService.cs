using ApplicationHealthServices.HealthService;

namespace DelegatesTest.Services
{
    public class SimpleHealthService : IHealthService
    {
        public ServiceHealthStatus GetServiceStatus()
        {
            return new ServiceHealthStatus(HealthStatusType.DatabaseConnection, true);
        }
    }
}