using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace POC1.Application.Extension
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}