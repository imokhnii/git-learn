using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompetitionPrizeController : ControllerBase
    {
        public CompetitionPrizeController(ICompetitionPrizeService prizeService, ILoggerFactory loggerFactory)
        {
            _prizeService = prizeService;
            logger = loggerFactory.CreateLogger("FileLogger");
            logger.LogInformation("Entered {0} Controller", "CompetitionPrize");
        }
        private readonly ICompetitionPrizeService _prizeService;
        private readonly ILogger logger;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompetitionPrizeRequest item)
        {
            if (item == null)
            {
                logger.LogInformation("Failed at creating new Competition Prize");
                return BadRequest();
            }
            var prize = await _prizeService.CreateAsync(item);
            logger.LogInformation("Created Prize: {0}", prize.Id);
            return CreatedAtRoute("GetCompetitionPrize", new { id = prize.Id }, prize);
        }

        [HttpPost]
        public async Task<IActionResult> AddPrizesToCompetition(int CompetitionId, int PrizeId)
        {
            if (ModelState.IsValid)
            {
                var prize = await _prizeService.AddPrizesToCompetitionAsync(CompetitionId, PrizeId);
                if (prize != null)
                {
                    logger.LogInformation("Prize {0} added to Competition {1}", PrizeId, CompetitionId);
                    return Ok(prize);
                }
            }
            logger.LogInformation("Failed at adding Prize {0} to Competition {1}", PrizeId, CompetitionId);
            return NotFound();
        }

        [HttpGet("{id}", Name = "GetCompetitionPrize")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _prizeService.FindByIdAsync(id);
            if (item == null)
            {
                logger.LogInformation("Failed at getting Prize {0}", id);
                return NotFound();
            }
            logger.LogInformation("Got by id Prize{0}", id);
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _prizeService.FindByIdAsync(id);
            if (item == null)
            {
                logger.LogInformation("Failed at deleting Prize{0}", id);
                return NotFound();
            }

            await _prizeService.DeleteAsync(id);
            logger.LogInformation("Deleted Prize {0}", id);
            return new NoContentResult();
        }
    }
}
