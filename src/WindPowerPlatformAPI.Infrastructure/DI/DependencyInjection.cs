using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Services;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services);
            AddRepositories(services);

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ICommandService, CommandService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository, CommandRepository>();
        }
    }
}
