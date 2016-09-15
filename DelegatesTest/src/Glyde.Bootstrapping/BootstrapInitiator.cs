using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Glyde.Bootstrapping
{
    public abstract class BootstrapInitiator<TBootstrapperContract>
        where TBootstrapperContract : IBootstrapper
    {
        public abstract void Run(IEnumerable<Assembly> assemblies);

        protected IEnumerable<TBootstrapperContract> GetBootstrappers(IEnumerable<Assembly> assemblies)
        {
            var result = new List<TBootstrapperContract>();
            var bootstrapperContractType = typeof(TBootstrapperContract);
            foreach (var assembly in assemblies)
            {
                var assemblyBootstrappers = assembly.GetTypes()
                    .Select(t => t.GetTypeInfo())
                    .Where(t => t.IsClass && !t.IsAbstract && t.ImplementedInterfaces.Contains(bootstrapperContractType))
                    .Select(t => (TBootstrapperContract)BootstrapInitiatorCache.Get(t.AsType()))
                    .ToList();

                result.AddRange(assemblyBootstrappers);
            }

            return result;
        }
    }
}