using System;
using System.Threading.Tasks;
using DelegatesTest.RequestContext;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DelegatesTest.Middleware
{
    public class RequestContextGeneratorMiddleware
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public RequestContextGeneratorMiddleware(RequestDelegate next, IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _loggerFactory = loggerFactory;
        }

        public Task Invoke(HttpContext context)
        {
            context.Items.Add(typeof (IRequestContext),
                new RequestContext.RequestContext(context, _serviceProvider));

            context.Response.Headers.Add("X-Zeppu-Version", "1.0");
            return _next(context);
        }
    }
}