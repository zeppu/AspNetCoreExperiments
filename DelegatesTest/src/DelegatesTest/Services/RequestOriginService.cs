using DelegatesTest.RequestContext;
using DelegatesTest.RequestContext.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace DelegatesTest.Services
{
    public class RequestOriginService : IRequestContextDataGenerator<IRequestOrigin>
    {
        private readonly IIpService _ipService;

        public RequestOriginService(IIpService ipService)
        {
            _ipService = ipService;
        }

        public IRequestOrigin GenerateData(HttpContext context)
        {
            return new RequestOrigin(
                context.Connection.RemoteIpAddress.ToString(),
                context.Request.Headers[HeaderNames.Referer],
                _ipService.GetCountry(context.Connection.RemoteIpAddress.ToString())
                );
        }
    }
}