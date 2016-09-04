namespace ApplicationHealthServices.HealthService
{
    public interface IHealthService
    {
        ServiceHealthStatus GetServiceStatus();
    }

    public interface IBackgroundHealthService : IHealthService
    {
        void Start();

        void Stop();
    }
}