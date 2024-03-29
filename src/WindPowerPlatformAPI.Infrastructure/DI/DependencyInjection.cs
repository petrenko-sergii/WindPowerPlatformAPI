﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Filters;
using WindPowerPlatformAPI.Infrastructure.Helpers;
using WindPowerPlatformAPI.Infrastructure.Helpers.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Services;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services);
            AddHelpers(services);
            AddRepositories(services);
            AddFilters(services);

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<ITurbineService, TurbineService>();
            services.AddScoped<ICloudFuncService, CloudFuncService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<ICloudFuncUrlBuilderService, CloudFuncUrlBuilderService>();
        }

        private static void AddHelpers(IServiceCollection services)
        {
            services.AddScoped<IAzureResponseHelper, AzureResponseHelper>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICommandRepository, CommandRepository>();
            services.AddScoped<ITurbineRepository, TurbineRepository>();
            services.AddScoped<ITurbineInfoFileRepository, TurbineInfoFileRepository>();
        }

        private static void AddFilters(IServiceCollection services)
        {
            services.AddTransient<UniqueSerialNumberFilter>();
        }
    }
}
