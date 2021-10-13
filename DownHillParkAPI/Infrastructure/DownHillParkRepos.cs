using DownHillParkAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DownHillParkAPI.Infrastructure
{
    public static class DownHillParkRepos
    {
        public static IServiceCollection AddDownHillParkRepos(this IServiceCollection services)
        {
            services.AddScoped<IBikeRepository, BikeRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ICompetitionRepository, CompetitionRepository>();
            services.AddScoped<ICompetitionPrizeRepository, CompetitionPrizeRepository>();
            services.AddScoped<ICompetitionResultRepository, CompetitionResultRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
