namespace DelegatesTest.Services
{
    internal class DummyIpService : IIpService
    {
        public string GetCountry(string ipAddress)
        {
            return "MT";
        }
    }
}