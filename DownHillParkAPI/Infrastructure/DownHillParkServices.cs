﻿using DownHillParkAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
