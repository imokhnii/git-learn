using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IBikeRepository _bikeManager;
        private readonly ITeamRepository _teamManager;
        private readonly ICompetitionRepository _competitionManager;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IBikeRepository bikeManager, ITeamRepository teamManager, ICompetitionRepository competitionManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bikeManager = bikeManager;
            _teamManager = teamManager;
            _competitionManager = competitionManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddBikeToUser(int BikeId, string UserId)
        {
            if (ModelState.IsValid)
            {
                var bike = _bikeManager.FindById(BikeId);
                var user = await _userManager.FindByIdAsync(UserId);
                if (user != null && bike != null)
                {
                    bike.User = user;
                    _bikeManager.Update(bike);
                    return Ok(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamToUser(int TeamId, string UserId)
        {
            if (ModelState.IsValid)
            {
                var team = _teamManager.FindById(TeamId);
                var user = await _userManager.FindByIdAsync(UserId);
                if (user != null && team != null)
                {
                    user.TeamId = TeamId;
                    _teamManager.Update(team);
                    return Ok(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddCountryToUser(string Country, string UserId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user != null && Country != null)
                {
                    user.Country = Country;
                    await _userManager.UpdateAsync(user);
                    return Ok(user);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToCompetition(int CompetitionId, string UserId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                var competition = _competitionManager.FindById(CompetitionId);
                if (user != null && competition != null)
                {
                    user.CompetitionId = CompetitionId;
                    _competitionManager.Update(competition);
                    return Ok(user);
                }
            }
            return NotFound();
        }
        //[HttpGet]
        //public async Task<User> GetByIdAsync(string UserId)
        //{
        //    var user = await _userManager.FindByIdAsync(UserId);
        //    return user;
        //}



    }
}
