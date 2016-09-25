using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glyde.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore;
using SimpleInjector.Integration.AspNetCore.Mvc;

namespace Glyde.Di.SimpleInjector
{
    public class SimpleInjectorDependencyInjection : DependencyInjectionBootstrapper
    {
        private readonly Container _container;
        private readonly SimpleInjectorConfigurationBuilder _configurationBuilder;

        public SimpleInjectorDependencyInjection()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();
            _configurationBuilder = new SimpleInjectorConfigurationBuilder(_container);
        }

        public override void RegisterWithApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSimpleInjectorAspNetRequestScoping(_container);

            // register http accessor with application DI
            _container.RegisterSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // register controllers and optionally any views present
            _container.RegisterMvcControllers(applicationBuilder);
            try
            {
                _container.RegisterMvcViewComponents(applicationBuilder);
            }
            catch (Exception e)
            {

            }
        }

        public override ServiceProviderBootstrapperInitiator CreateServiceProviderBootstrapperInitiator(IServiceCollection serviceCollection,
            IConfigurationProvider configurationProvider)
        {
            serviceCollection.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            serviceCollection.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));

            // this registration with the framework DI is required - internal framework stuff depends on it (in most cases as an optional dep).
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return new SimpleInjectorServiceProviderBootstrapperInitiator(_configurationBuilder);
        }
    }
}
