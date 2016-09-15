﻿namespace Glyde.Di
{
    public interface IServiceProviderConfigurator
    {
        void AddTransientService<TContract, TService>()
            where TService : class, TContract where TContract : class;
        void AddSingletonService<TContract, TService>()
            where TService : class, TContract where TContract : class;
        void AddScopedService<TContract, TService>()
            where TService : class, TContract where TContract : class;
    }
}