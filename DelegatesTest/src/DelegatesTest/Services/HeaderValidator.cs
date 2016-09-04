using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DelegatesTest.Services
{
    public class HeaderValidator : IRequestValidator
    {
        private readonly string[] _requiredHeaders;

        public HeaderValidator(params string[] requiredHeaders)
        {
            _requiredHeaders = requiredHeaders;
        }

        public bool IsValid(HttpRequest request)
        {
            return _requiredHeaders.All(header => request.Headers.ContainsKey(header));
        }
    }
}