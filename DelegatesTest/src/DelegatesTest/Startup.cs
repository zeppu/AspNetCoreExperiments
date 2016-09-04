using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DelegatesTest.Extensions;
using DelegatesTest.Middleware;
using DelegatesTest.RequestContext;
using DelegatesTest.RequestContext.Data;
using DelegatesTest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddMvc();
            services.UseRequestGenerators(Assembly.GetEntryAssembly());

            services.AddSingleton<IIpService, DummyIpService>();
            services.AddTransient<IRequestValidator>(provider => new HeaderValidator("X-Zeppu-Id", "X-Zeppu-Token"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseMiddleware<HeaderValidationMiddleware>();
            app.UseMiddleware<RequestContextGeneratorMiddleware>();
            app.UseMvc();
        }
    }
}