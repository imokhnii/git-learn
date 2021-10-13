using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
                if (user != null)
                {
                    logger.LogInformation("Bike {0} added to User: {1}", BikeId, UserId);
                    return Ok(user);
                }
            }
            logger.LogInformation("Failed at adding Bike {0} to User: {1}", BikeId, UserId);
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
                    logger.LogInformation("User: {0} added to Team {1}", UserId, TeamId);
                    return Ok(user);
                }
            }
            logger.LogInformation("Failed at adding User: {0} to Team {1}", UserId, TeamId);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddCountryToUser(string Country, string UserId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.AddCountryToUserAsync(Country, UserId);
                if (user != null)
                {
                    logger.LogInformation("Country {0} added to User: {1}", Country, UserId);
                    return Ok(user);
                }
            }
            logger.LogInformation("Failed at adding Country {0} to User: {1}", Country, UserId);
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
                    logger.LogInformation("User: {0} added to Competition {1}", UserId, CompetitionId);
                    return Ok(user);
                }
            }
            logger.LogInformation("Failed at adding User: {0} to Competition {1}", UserId, CompetitionId);
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(string UserId)
        {
            var user = await _userService.FindByIdAsync(UserId);
            if (user != null)
            {
                logger.LogInformation("Got by id User: {0}", UserId);
                return Ok(user);
            }
            logger.LogInformation("Failed at getting User: {0}", UserId);
            return NotFound();
        }



    }
}
