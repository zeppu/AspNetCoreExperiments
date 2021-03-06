﻿using System.Linq;
using System.Reflection;
using ApplicationHealthServices;
using DelegatesTest.Extensions;
using DelegatesTest.Middleware;
using DelegatesTest.Services;
using Glyde.Core.Requests.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var assemblyParts = DefaultAssemblyPartDiscoveryProvider.DiscoverAssemblyParts(thisAssembly.FullName);

            var assemblies = assemblyParts.Select(p => Assembly.Load(new AssemblyName(p.Name))).ToList();

            services.AddApplicationHealthServices(assemblies);
            services.UseRequestGenerators(assemblies);
            services.AddSingleton<IIpService, DummyIpService>();
            services.AddScoped<IRequestValidator>(provider => new HeaderValidator("X-Zeppu-Id", "X-Zeppu-Token"));
            services.AddMvcCore()
                .AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseApplicationHealthServices();
            app
                .UseMiddleware<ErrorHandlingMiddleware>()
//                .UseMiddleware<HeaderValidationMiddleware>()
                .UseForwardedHeaders()
                .UseRequestContextGenerator()
                .UseMvc();
        }
    }
}