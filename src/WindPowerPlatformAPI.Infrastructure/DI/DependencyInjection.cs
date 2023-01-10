using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WindPowerPlatformAPI.Infrastructure.Repositories;
using WindPowerPlatformAPI.Infrastructure.Repositories.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //AddServices(services);
            AddRepositories(services);

            return services;
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository, MockCommandRepository>();
        }
    }
}
