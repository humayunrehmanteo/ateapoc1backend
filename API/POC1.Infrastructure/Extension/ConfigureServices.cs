using Microsoft.Extensions.DependencyInjection;
using POC1.Application.Interfaces;
using POC1.Infrastructure.Repositories;

namespace POC1.Infrastructure.Extension
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureDependency(this IServiceCollection services)
        {
            services.AddScoped<IApiBlobsQueryRepository, ApiBlobsQueryRepository>();
            services.AddScoped<IApiLogsQueryRepository, ApiLogsQueryRepository>();
            return services;
        }
    }

}
