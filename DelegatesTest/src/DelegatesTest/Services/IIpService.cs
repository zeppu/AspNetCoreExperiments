using System.Threading.Tasks;

namespace DelegatesTest.Services
{
    public interface IIpService
    {
        string GetCountry(string ipAddress);

        Task<string> GetCountryAsync(string ipAddress);
    }
}