using DownHillParkAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DownHillParkAPI.Infrastructure
{
    public static class DownHillParkRepos
    {
        public static IServiceCollection AddDownHillParkRepos(this IServiceCollection services)
        { 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
