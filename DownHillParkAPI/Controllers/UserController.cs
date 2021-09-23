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
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IBikeRepository bikeManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bikeManager = bikeManager;
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




    }
}
