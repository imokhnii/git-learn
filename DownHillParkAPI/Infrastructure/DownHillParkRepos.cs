using DownHillParkAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return services;
        }
    }
}
