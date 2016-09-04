using Glyde.Core.DeviceDetection.Models;
using Glyde.Core.Requests;

namespace Glyde.Core.DeviceDetection.Extensions
{
    public static class RequestContextExtensions
    {
        public static IDeviceInformation GetDeviceInformation(this IRequestContext context)
        {
            return context.Get<IDeviceInformation>();
        }
    }
}