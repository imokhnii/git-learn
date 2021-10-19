using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompetitionResultController : ControllerBase
    {
        private readonly ICompetitionResultService _resultService;
        public CompetitionResultController(ICompetitionResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ResultRequest item)
        {
            var result = await _resultService.Create(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        
        [HttpGet]
        public IActionResult FindWinner(int CompetitionId)
        {
            var result = _resultService.CalculateWinner(_resultService.GetByCompId(CompetitionId));
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
