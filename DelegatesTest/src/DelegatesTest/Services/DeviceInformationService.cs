using DelegatesTest.RequestContext;
using DelegatesTest.RequestContext.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace DelegatesTest.Services
{
    public class DeviceInformationService : IRequestContextDataGenerator<IDeviceInformation>
    {
        public IDeviceInformation GenerateData(HttpContext context)
        {
            var ua = context.Request.Headers[HeaderNames.UserAgent];
            return new DeviceInformation(ua);
        }
    }
}