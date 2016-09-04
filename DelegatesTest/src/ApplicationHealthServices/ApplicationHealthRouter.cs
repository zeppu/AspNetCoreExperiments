using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApplicationHealthServices.HealthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace ApplicationHealthServices
{
    internal class ApplicationHealthRouter : IRouter
    {
        private readonly HealthServiceManager _healthServiceManager;
        private readonly JsonSerializer _serializer = JsonSerializer.Create(new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        });

        public ApplicationHealthRouter(HealthServiceManager healthServiceManager)
        {
            _healthServiceManager = healthServiceManager;
        }

        public Task RouteAsync(RouteContext context)
        {
            context.Handler = Handler;
            return Task.CompletedTask;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        private Task Handler(HttpContext context)
        {
            var section = (string) context.GetRouteValue("op");

            switch (section)
            {
                case null:
                case "status":
                    context.Response.StatusCode = (int) HttpStatusCode.OK;
                    break;
                case "detailed":
                {
                    context.Response.StatusCode = (int) HttpStatusCode.OK;
                    using (var sw = new StreamWriter(context.Response.Body))
                    using (var writer = new JsonTextWriter(sw))
                    {
                        _serializer.Serialize(writer, _healthServiceManager.GetHealthServicesStatus());
                    }
                }
                    break;
                default:
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
            }

            return Task.CompletedTask;
        }
    }
}