using ApplicationHealthServices.HealthService;

namespace DelegatesTest.Services
{
    public class BackgroundHealthService : IBackgroundHealthService
    {
        public ServiceHealthStatus GetServiceStatus()
        {
            return new ServiceHealthStatus(HealthStatusType.DatabaseConnection, true);
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}