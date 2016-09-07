using System;
using System.Threading.Tasks;

namespace DelegatesTest.Services
{
    internal class DummyIpService : IIpService
    {
        public string GetCountry(string ipAddress)
        {
            return "MT";
        }

        public async Task<string> GetCountryAsync(string ipAddress)
        {
//            throw new Exception("oqo");
            await Task.Delay(TimeSpan.FromSeconds(1));
            return "MT";
        }
    }
}