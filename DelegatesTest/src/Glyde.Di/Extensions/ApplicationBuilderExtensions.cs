using Microsoft.AspNetCore.Builder;

namespace Glyde.Di
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDependencyInjection<TDiBootstrapper>(
            this IApplicationBuilder applicationBuilder,
            TDiBootstrapper diBootstrapper)
            where TDiBootstrapper : DependencyInjectionBootstrapper, new()
        {
            diBootstrapper.RegisterWithApplication(applicationBuilder);
            return applicationBuilder;
        }

    }
}