using Microsoft.AspNetCore.Http;

namespace DelegatesTest.Services
{
    public interface IRequestValidator
    {
        bool IsValid(HttpRequest request);
    }
}