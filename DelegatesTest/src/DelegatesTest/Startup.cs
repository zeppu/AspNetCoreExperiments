using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ApplicationHealthServices;
using DelegatesTest.Extensions;
using DelegatesTest.Middleware;
using DelegatesTest.Services;
using Glyde.Core.Requests.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;

namespace DelegatesTest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            var thisAssembly = Assembly.GetEntryAssembly();

            var dependencyContext = DependencyContext.Load(thisAssembly);
            var ownAssemblies = dependencyContext.RuntimeLibraries
                .Where(r => r.Name.StartsWith("Glyde"))
                .SelectMany( l => l.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load) )
                .ToList();
            ownAssemblies.Add(thisAssembly);


            //var assemblyParts = DefaultAssemblyPartDiscoveryProvider.DiscoverAssemblyParts(thisAssembly.FullName);

            //var assemblies = assemblyParts.Select(p => Assembly.Load(new AssemblyName(p.Name))).ToList();

            services.AddApplicationHealthServices(ownAssemblies);
            services.UseRequestGenerators(ownAssemblies);
            services.AddSingleton<IIpService, DummyIpService>();
            services.AddScoped<IRequestValidator>(provider => new HeaderValidator("X-Zeppu-Id", "X-Zeppu-Token"));
            services.AddMvcCore(options => options.Conventions.Insert(0, new ApiPrefixConvention("api/v[version]")))
                .AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app
                .UseApplicationHealthServices(routePrefix: "api")
                .UseMiddleware<ErrorHandlingMiddleware>()
//                .UseMiddleware<HeaderValidationMiddleware>()
                .UseForwardedHeaders()
                .UseRequestContextGenerator()
                .UseMvc();
        }
    }

    public class VersionAttribute : Attribute
    {
        public int Version { get; }

        public VersionAttribute(int version)
        {
            Version = version;
        }
    }

    public class ApiPrefixConvention : IApplicationModelConvention
    {
        private readonly string _prefix;

        public ApiPrefixConvention(string prefix)
        {
            _prefix = prefix.ToLower();
        }

        public void Apply(ApplicationModel application)
        {
            AttributeRouteModel prefixRouteModel = null;
            var requiresVersion = true;
            if (!_prefix.Contains("[version]"))
            {
                prefixRouteModel = new AttributeRouteModel(new RouteAttribute(_prefix));
                requiresVersion = false;
            }

            foreach (var controller in application.Controllers)
            {
                if (requiresVersion)
                {
                    var version = 1;
                    var versionAttribute =
                        controller.Attributes.FirstOrDefault(m => m is VersionAttribute) as VersionAttribute;
                    if (versionAttribute != null)
                        version = versionAttribute.Version;

                    prefixRouteModel =
                        new AttributeRouteModel(new RouteAttribute(_prefix.Replace("[version]", version.ToString())));
                }

                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectorModel in matchedSelectors)
                    {
                        selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(prefixRouteModel,
                            selectorModel.AttributeRouteModel);
                    }
                }

                var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                if (unmatchedSelectors.Any())
                {
                    foreach (var selectorModel in unmatchedSelectors)
                    {
                        selectorModel.AttributeRouteModel = prefixRouteModel;
                    }
                }
            }
        }
    }
}