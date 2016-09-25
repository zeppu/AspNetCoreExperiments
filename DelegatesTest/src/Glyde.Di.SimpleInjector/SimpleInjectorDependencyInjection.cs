using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glyde.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
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
            _configurationBuilder = new SimpleInjectorConfigurationBuilder(_container);
        }

        public override void RegisterWithApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSimpleInjectorAspNetRequestScoping(_container);

            _container.RegisterMvcControllers(applicationBuilder);
            _container.RegisterMvcViewComponents(applicationBuilder);
        }

        public override ServiceProviderBootstrapperInitiator CreateServiceProviderBootstrapperInitiator(IServiceCollection serviceCollection,
            IConfigurationProvider configurationProvider)
        {
            serviceCollection.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            serviceCollection.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));

            return new SimpleInjectorServiceProviderBootstrapperInitiator(_configurationBuilder);
        }
    }
}
