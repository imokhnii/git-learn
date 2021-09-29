using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.RequestModels;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var newUser = await _userManager.FindByEmailAsync(user.Email);

                    await _userManager.AddToRolesAsync(newUser, new[] { "User" }); //error 500 System.InvalidOperationException: Role USER does not exist.
                    return Ok(newUser);
                }

            }
            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser model)
        {

            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded)
                {

                    var user = await _userManager.FindByEmailAsync(model.Email);
                    await _signInManager.SignInAsync(user, false);

                    if (user != null)
                    {
                        return Ok(user);
                    }
                }

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(true);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangeSelfPassword([FromBody] ChangePassword modal)
        {
            var user = await _userManager.FindByEmailAsync(modal.Email);
            IdentityResult passwordChangeResult = await _userManager.ChangePasswordAsync(user, modal.CurrentPasword, modal.NewPassword);
            if (passwordChangeResult.Succeeded)
            {
                return Ok(passwordChangeResult.Succeeded);
            }

            return NotFound();
        }

        [HttpPost("/token")]
        public async Task<IActionResult> TokenAsync(string username, string password)
        {
            var identity = await GetIdentityAsync(username, password);
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
        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            User user = _userManager.Users.FirstOrDefault(x => x.UserName == username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, password,false);
            if (user != null && result == Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
