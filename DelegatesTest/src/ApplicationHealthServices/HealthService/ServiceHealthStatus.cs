using System;

namespace ApplicationHealthServices.HealthService
{
    public class ServiceHealthStatus
    {
        private static readonly Object Empty = new object();

        public ServiceHealthStatus(HealthStatusType type, bool isHealthy, object data = null)
        {
            Type = type;
            IsHealthy = isHealthy;
            Data = data ?? Empty;
        }

        public HealthStatusType Type { get; }

        public object Data { get; }

        public bool IsHealthy { get; }
    }
}