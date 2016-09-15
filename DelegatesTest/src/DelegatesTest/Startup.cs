using System;
using System.Linq;
using System.Reflection;
using ApplicationHealthServices;
using DelegatesTest.Extensions;
using DelegatesTest.Middleware;
using DelegatesTest.Services;
using Glyde.Bootstrapping;
using Glyde.Configuration;
using Glyde.Core.Requests.Middleware;
using Glyde.Di;
using Glyde.Di.AspNetDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using ConfigurationProvider = Glyde.Configuration.ConfigurationProvider;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
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

            var configurationProvider = new ConfigurationProvider();

            return services.RegisterAllServices<AspNetServiceProviderBootstrapperInitiator>(ownAssemblies,
                configurationProvider);
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
}