using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DelegatesTest.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            try
            {
                return _next(context);
            }
            catch (Exception e)
            {
                return context.Response.WriteAsync(e.Message);
            }
        }
    }
}