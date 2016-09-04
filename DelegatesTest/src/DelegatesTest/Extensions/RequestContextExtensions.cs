using DelegatesTest.RequestContext;
using DelegatesTest.RequestContext.Data;

namespace DelegatesTest.Extensions
{
    public static class RequestContextExtensions
    {
        public static IDeviceInformation GetDeviceInformation(this IRequestContext context)
        {
            return context.Get<IDeviceInformation>();
        }
    }
}