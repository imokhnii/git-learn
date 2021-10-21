using DownHillParkAPI.Models;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/competitions/[action]")]
    [ApiController]
    public class CompetitionController : Controller
    {
        public CompetitionController(ICompetitionService competitionService, ILoggerFactory loggerFactory, ICompetitionPrizeService prizeService)
        {
            _competitionService = competitionService;
            _prizeService = prizeService;
            logger = loggerFactory.CreateLogger("FileLogger");
            logger.LogInformation("Entered {0} Controller", "Competition");
        }
        private readonly ICompetitionService _competitionService;
        private readonly ICompetitionPrizeService _prizeService;
        private readonly ILogger logger;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompetitionRequest item)
        {
            if (item == null)
            {
                logger.LogInformation("Failed at creating new Competition {0}", item.Name);
                return BadRequest();
            }
            var competition = await _competitionService.CreateAsync(item);
            logger.LogInformation("Created Competition: {0}", competition.Id);
            return CreatedAtRoute("GetCompetition", new { id = competition.Id }, competition);
        }

        [HttpGet]
        public IEnumerable<Competition> GetAll([FromQuery]PageRequest pageRequest)
        {
            logger.LogInformation("Got all competitions");
            return _competitionService.GetAll(pageRequest);
        }
        [HttpGet("{id}", Name = "GetCompetition")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _competitionService.FindByIdAsync(id);
            if (item == null)
            {
                logger.LogInformation("Failed at getting Competition {0}", id);
                return NotFound();
            }
            logger.LogInformation("Got by id Competition: {0}", id);
            return new ObjectResult(item);
        }
        [HttpPost]
        public async Task<IActionResult> AddPrizesToCompetition(int CompetitionId, [FromBody] CompetitionPrizeRequest item)
        {
            if (item == null)
            {
                logger.LogInformation("Failed at creating new Competition Prize");
                return BadRequest();
            }
            var prize = await _prizeService.CreateAsync(item);
            logger.LogInformation("Created Prize: {0}", prize.Id);
            var addedPrize = await _prizeService.AddPrizesToCompetitionAsync(CompetitionId, prize.Id);
            if (addedPrize != null)
            {
                logger.LogInformation("Prize {0} added to Competition {1}", addedPrize.Id, CompetitionId);
                return Ok(addedPrize);
            }
            logger.LogInformation("Failed at adding Prize {0} to Competition {1}", prize.Id, CompetitionId);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var item = await _competitionService.FindByIdAsync(id);
            if (item == null)
            {
                logger.LogInformation("Failed at deleting Competition {0}", id);
                return NotFound();
            }

            await _competitionService.DeleteAsync(id);
            logger.LogInformation("Deleted Competition: {0}", id);
            return new NoContentResult();
        }
    }
}
