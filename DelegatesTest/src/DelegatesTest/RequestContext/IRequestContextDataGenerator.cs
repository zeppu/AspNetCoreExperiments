using Microsoft.AspNetCore.Http;

namespace DelegatesTest.RequestContext
{
    public interface IRequestContextDataGenerator<out T> where T : class, IRequestData
    {
        T GenerateData(HttpContext context);
    }
}