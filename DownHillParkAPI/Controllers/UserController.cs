using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IUserService _userService;
        private readonly ILogger logger;
        public UserController(IUserService userService, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            logger = loggerFactory.CreateLogger("FileLogger");
            logger.LogInformation("Entered {0} Controller", "User");
        }

        [HttpPost]
        public async Task<IActionResult> AddBikeToUser(int BikeId, string UserId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.AddBikeToUserAsync(BikeId, UserId);
                logger.LogInformation("Bike {0} added to User: {1}", BikeId, UserId);
                if (user != null)
                {
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
                var user = await _userService.AddTeamToUserAsync(TeamId, UserId);
                logger.LogInformation("User: {0} added to Team {1}", UserId, TeamId);
                if (user != null)
                {
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
                var user = await _userService.AddCountryToUserAsync(Country, UserId);
                logger.LogInformation("Country {0} added to User: {1}", Country, UserId);
                if (user != null)
                {
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
                var user = await _userService.AddUserToCompetitionAsync(CompetitionId, UserId);
                logger.LogInformation("User: {0} added to competition {1}", UserId, CompetitionId);
                if (user != null)
                {
                    return Ok(user);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(string UserId)
        {
            var user = await _userService.FindByIdAsync(UserId);

            logger.LogInformation("User: {0}", UserId);

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }



    }
}
