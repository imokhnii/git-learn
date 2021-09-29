using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
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
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBikeToUser(int BikeId, string UserId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.AddBikeToUserAsync(BikeId, UserId);
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
                var user = _userService.AddCountryToUserAsync(Country, UserId);
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
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }



    }
}
