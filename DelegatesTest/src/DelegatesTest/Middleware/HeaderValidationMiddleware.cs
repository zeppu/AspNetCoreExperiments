using System;
using System.Threading.Tasks;
using DelegatesTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DelegatesTest.Middleware
{
    public class HeaderValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestValidator _requestValidator;
        private readonly ILoggerFactory _loggerFactory;

        public HeaderValidationMiddleware(RequestDelegate next, IRequestValidator requestValidator,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _requestValidator = requestValidator;
            _loggerFactory = loggerFactory;
        }

        public Task Invoke(HttpContext context)
        {
            var valid = _requestValidator.IsValid(context.Request);
            context.Response.Headers.Add("X-Zeppu-Version", "1.0");
            if (valid) return _next(context);
            
            throw new Exception("Bad headers!");
        }
    }
}