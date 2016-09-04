using Microsoft.AspNetCore.Http;

namespace Glyde.Core.Requests
{
    public interface IRequestContextDataGenerator<out T> where T : class, IRequestData
    {
        T GenerateData(HttpContext context);
    }
}