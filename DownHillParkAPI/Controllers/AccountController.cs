using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly ILogger logger;
        public AccountController(IAccountService accountService, ILoggerFactory loggerFactory)
        {
            _accountService = accountService;
            logger = loggerFactory.CreateLogger("FileLogger");
            logger.LogInformation("Entered {0} Controller", "Account");
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.RegisterAsync(model);
                if(user != null)
                {
                    logger.LogInformation("Registered User: {0}", user.Id);
                    return Ok(user);
                }

            }
            logger.LogInformation("Failed at registering User {0}", model.Email);
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
                    logger.LogInformation("Logged in User: {0}", user.Id);
                    return Ok(user);
                }

            }
            logger.LogInformation("Failed at login User {0}", model.Email);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _accountService.LogoutAsync();
            logger.LogInformation("Logged out User: {0}", userid);
            return Ok(true);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangeSelfPassword([FromBody] ChangePassword modal)
        {
            IdentityResult passwordChangeResult = await _accountService.ChangeSelfPasswordAsync(modal);
            if (passwordChangeResult.Succeeded)
            {
                logger.LogInformation("Changed password for User: {0}", modal.Email);
                return Ok(passwordChangeResult.Succeeded);
            }
            logger.LogInformation("Failed at changing password for User: {0}", modal.Email);
            return NotFound();
        }

        [HttpPost("/token")]
        public async Task<IActionResult> TokenAsync(string username, string password)
        {
            var identity = await _accountService.GetIdentityAsync(username, password);
            if (identity == null)
            {
                logger.LogInformation("Failed at creating token for User: {0}", username);
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
            logger.LogInformation("Created token for User: {0}", username);
            return Json(response);
        }
        
    }
}
