using DownHillParkAPI.Models;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/teams/[action]")]
    public class TeamController : ControllerBase
    {
        public TeamController(ITeamService teamService, ILoggerFactory loggerFactory)
        {
            _teamService = teamService;
            logger = loggerFactory.CreateLogger("FileLogger");
            logger.LogInformation("Entered {0} Controller", "Team");
        }
        private readonly ITeamService _teamService;
        private readonly ILogger logger;

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] TeamRequest item)
        {
            if (item == null)
            {
                logger.LogInformation("Failed at creating new Team {0}",item.Name);
                return BadRequest();
            }
            var team = await _teamService.CreateAsync(item);
            logger.LogInformation("Created Team: {0}", team.Id);
            return CreatedAtRoute("GetTeam", new { id = team.Id }, team);
        }

        [HttpGet]
        public IEnumerable<Team> GetAll()
        {
            logger.LogInformation("Got all teams");
            return _teamService.GetAll();
        }

        [HttpGet("{id}", Name = "GetTeam")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _teamService.FindByIdAsync(id);
            if (item == null)
            {
                logger.LogInformation("Failed at getting Team {0}", id);
                return NotFound();
            }
            logger.LogInformation("Got by id Team: {0}", id);
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _teamService.FindByIdAsync(id);
            if (item == null)
            {
                logger.LogInformation("Failed at deleting Team {0}", id);
                return NotFound();
            }
            await _teamService.DeleteAsync(id);
            logger.LogInformation("Deleted Team: {0}", id);
            return new NoContentResult();
        }
    }
}
