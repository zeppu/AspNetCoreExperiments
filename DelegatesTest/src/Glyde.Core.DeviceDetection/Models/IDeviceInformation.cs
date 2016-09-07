using Glyde.Core.Requests;

namespace Glyde.Core.DeviceDetection.Models
{
    public interface IDeviceInformation : IRequestData
    {
        string UserAgent { get; set; }
    }
}