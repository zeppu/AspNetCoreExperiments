using System;
using System.Threading.Tasks;

namespace DelegatesTest.RequestContext.Data
{
    public class AsyncRequestOrigin : IRequestOrigin
    {
        private readonly Task<string> _countryCodeTask;
        private string _countryCode;
        private bool _countryCodeTaskCompleted;
        public string IpAddress { get; }
        public string Referrer { get; }

        public string CountryCode
        {
            get
            {
                if (_countryCodeTaskCompleted)
                    return _countryCode;
                try
                {
                    _countryCodeTask.Wait();
                    _countryCode = _countryCodeTask.Result;
                }
                catch { }
                _countryCodeTaskCompleted = true;
                return _countryCode;
            }
        }

        public AsyncRequestOrigin(string ipAddress, string referrer, Task<string> countryCodeTask)
        {
            _countryCodeTask = countryCodeTask;
            IpAddress = ipAddress;
            Referrer = referrer;
        }
    }
}