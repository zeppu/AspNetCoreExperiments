namespace DelegatesTest.RequestContext.Data
{
    public class RequestOrigin : IRequestOrigin
    {
        public RequestOrigin(string ipAddress, string referrer, string countryCode)
        {
            IpAddress = ipAddress;
            Referrer = referrer;
            CountryCode = countryCode;
        }

        public string IpAddress { get; }
        public string Referrer { get; }
        public string CountryCode { get; }
    }
}