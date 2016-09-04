using DelegatesTest.RequestContext;
using Microsoft.AspNetCore.Http;

namespace DelegatesTest.Extensions
{
    public static class HttpContextExtensions
    {
        public static IRequestContext GetRequestContext(this HttpContext context)
        {
            return context.Items[typeof(IRequestContext)] as IRequestContext;
        }
    }
}