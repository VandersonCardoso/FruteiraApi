using FruteiraApi.Core.Interfaces;
using FruteiraApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FruteiraApi.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IFrutaService, FrutaService>();
            #endregion

            return services;
        }

    }
}
