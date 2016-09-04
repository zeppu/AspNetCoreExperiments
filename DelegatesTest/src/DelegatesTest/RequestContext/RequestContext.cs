using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DelegatesTest.RequestContext
{
    public class RequestContext : IRequestContext
    {
        private readonly HttpContext _context;
        private readonly IServiceProvider _serviceProvider;

        public TData Get<TData>() where TData : class, IRequestData
        {
            var service = _serviceProvider.GetService<IRequestContextDataGenerator<TData>>();
            return service?.GenerateData(_context);
        }

        public RequestContext(HttpContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
    }
}