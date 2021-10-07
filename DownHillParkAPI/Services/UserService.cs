
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Services
{
    public interface IUserService
    {
        Task<User> AddBikeToUserAsync(int BikeId, string UserId);
        Task<User> AddTeamToUserAsync(int TeamId, string UserId);
        Task<User> AddCountryToUserAsync(string Country, string UserId);
        Task<User> AddUserToCompetitionAsync(int CompetitionId, string UserId);
        Task<User> FindByIdAsync(string id);
    }
    public class UserService : IUserService
    {
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IBikeService bikeService, ITeamService teamService, ICompetitionService competitionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bikeService = bikeService;
            _teamService = teamService;
            _competitionService = competitionService;
        }
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IBikeService _bikeService;
        private readonly ITeamService _teamService;
        private readonly ICompetitionService _competitionService;

        public async Task<User> AddBikeToUserAsync(int BikeId, string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var bike = await _bikeService.FindByIdAsync(BikeId);
            if (user != null & bike != null)
            {
                bike.User = user;
                await _bikeService.UpdateAsync(bike);
                return user;
            }
            return null;
        }

        public async Task<User> AddTeamToUserAsync(int TeamId, string UserId)
        {
            var team = await _teamService.FindByIdAsync(TeamId);
            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null && team != null)
            {
                user.TeamId = TeamId;
                await _teamService.UpdateAsync(team);
                return user;
            }
            return null;
        }

        public async Task<User> AddCountryToUserAsync(string Country, string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if(user !=null && Country != null)
            {
                user.Country = Country;
                await _userManager.UpdateAsync(user);
                return user;
            }
            return null;
        }

        public async Task<User> AddUserToCompetitionAsync(int CompetitionId, string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var competition = await _competitionService.FindByIdAsync(CompetitionId);
            if (user != null && competition != null)
            {
                user.CompetitionId = CompetitionId;
                await _competitionService.UpdateAsync(competition);
                return user;
            }
            return null;
        }

        public async Task<User> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                return user;
            }
            return null;
        }
    }
}
