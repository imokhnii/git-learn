using DownHillParkAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DownHillParkAPI.Infrastructure
{
    public static class DownHillParkServices
    {
        public static IServiceCollection AddDownHillParkServices(this IServiceCollection services)
        {
            services.AddDownHillParkRepos();

            services.AddScoped<IBikeService, BikeService>();
            services.AddScoped<ICompetitionService, CompetitionService>();
            services.AddScoped<ICompetitionPrizeService, CompetitionPrizeService>();
            services.AddScoped<ICompetitionResultService, CompetitionResultService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
