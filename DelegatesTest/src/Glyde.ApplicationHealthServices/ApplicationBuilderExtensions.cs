using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationHealthServices
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationHealthServices(this IApplicationBuilder app, string routePrefix = "", string endPointName = "health")
        {
            var routeBuilder = new RouteBuilder(app)
            {
                DefaultHandler = (IRouter) app.ApplicationServices.GetRequiredService<ApplicationHealthRouter>()
            };

            // normalize route prefix and endpoint names
            routePrefix = routePrefix ?? string.Empty;
            if (!string.IsNullOrEmpty(routePrefix))
                routePrefix = routePrefix.Trim('/') + '/';

            endPointName = endPointName ?? "health";
            endPointName = endPointName.Trim('/') + '/';


            routeBuilder.MapRoute("application-health-default", $"{routePrefix}{endPointName}");
            routeBuilder.MapRoute("application-health-route", $"{routePrefix}{endPointName}"+"{op:regex(status|detailed)}");

            app.UseRouter(routeBuilder.Build());

            return app;
        }
    }
}