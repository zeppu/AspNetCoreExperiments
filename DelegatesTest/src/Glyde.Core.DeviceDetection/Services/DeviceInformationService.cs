using Glyde.Core.DeviceDetection.Models;
using Glyde.Core.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Glyde.Core.DeviceDetection.Services
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