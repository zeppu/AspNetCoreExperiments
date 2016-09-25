﻿using System;
using System.Threading.Tasks;
using DelegatesTest.RequestContext.Data;
using Glyde.Core.DeviceDetection.Extensions;
using Glyde.Core.Requests;
using Glyde.Core.Requests.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DelegatesTest.Controllers
{
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        private readonly IRequestContext _requestContext;

        public ValuesController(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            //var ctx = HttpContext.GetRequestContext();
            var result = new
            {
                requestOrigin = _requestContext.Get<IRequestOrigin>(),
                deviceInformation = _requestContext.GetDeviceInformation()
            };
            return Ok(result);
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
