using DelegatesTest.RequestContext.Data;
using Glyde.Core.DeviceDetection.Extensions;
using Glyde.Core.Requests.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DelegatesTest.Controllers.v2
{
    [Version(2)]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var ctx = HttpContext.GetRequestContext();
            var result = new
            {
                requestOrigin = ctx.Get<IRequestOrigin>().IpAddress,
                deviceInformation = ctx.GetDeviceInformation()
            };
            return Ok(result);
        }

    }
}