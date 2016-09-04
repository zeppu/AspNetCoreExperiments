using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegatesTest.Extensions;
using DelegatesTest.RequestContext.Data;
using Microsoft.AspNetCore.Mvc;

namespace DelegatesTest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var ctx = HttpContext.GetRequestContext();
            return Ok(new
            {
                requestOrigin = ctx.Get<IRequestOrigin>(),
                deviceInformation = ctx.GetDeviceInformation()
            });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
