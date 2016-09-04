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
        public static void UseApplicationHealthServices(this IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app)
            {
                DefaultHandler = (IRouter) app.ApplicationServices.GetRequiredService<ApplicationHealthRouter>()
            };
            routeBuilder.MapRoute("application-health-default", "health/");
            routeBuilder.MapRoute("application-health-route", "health/{section}");

            app.UseRouter(routeBuilder.Build());
        }
    }
}