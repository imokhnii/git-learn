using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Authorization;
//using DownHillParkAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TokenApp;

namespace DownHillParkAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.RegisterAsync(model);
                if(user != null)
                {
                    return Ok(user);
                }

            }
            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser model)
        {

            if (ModelState.IsValid)
            {
                var user = await _accountService.LoginAsync(model);
                if(user != null)
                {
                    return Ok(user);
                }

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return Ok(true);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangeSelfPassword([FromBody] ChangePassword modal)
        {
            IdentityResult passwordChangeResult = await _accountService.ChangeSelfPasswordAsync(modal);
            if (passwordChangeResult.Succeeded)
            {
                return Ok(passwordChangeResult.Succeeded);
            }
            return NotFound();
        }

        [HttpPost("/token")]
        public async Task<IActionResult> TokenAsync(string username, string password)
        {
            var identity = await _accountService.GetIdentityAsync(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }
        
    }
}
