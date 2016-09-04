using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.Core.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace DelegatesTest.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void UseRequestGenerators(this IServiceCollection serviceCollection, IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var assemblyTypes = assembly.GetTypes();

                var dataGeneratorImplementations = assemblyTypes
                    .Select(t => t.GetTypeInfo())
                    .Where(typeinfo => typeinfo.IsClass)
                    .Select(typeInfo => new
                    {
                        Type = typeInfo.AsType(),
                        RegistrationIntf = typeInfo.GetInterfaces().FirstOrDefault(t => t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(IRequestContextDataGenerator<>))
                    })
                    .Where(x => x.RegistrationIntf != null)
                    .ToList();
                foreach (var implementationType in dataGeneratorImplementations)
                {
                    serviceCollection.AddTransient(implementationType.RegistrationIntf, implementationType.Type);
                }
            }
        }
    }
}