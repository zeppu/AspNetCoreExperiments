namespace DelegatesTest.RequestContext.Data
{
    public class DeviceInformation : IDeviceInformation
    {
        public DeviceInformation(string userAgent)
        {
            UserAgent = userAgent;
        }

        public string UserAgent { get; set; }
    }
}