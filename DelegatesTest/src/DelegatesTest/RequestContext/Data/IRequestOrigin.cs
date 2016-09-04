namespace DelegatesTest.RequestContext.Data
{
    public interface IRequestOrigin : IRequestData
    {
        string IpAddress { get; }
        string Referrer { get; }
        string CountryCode { get; }
    }
}