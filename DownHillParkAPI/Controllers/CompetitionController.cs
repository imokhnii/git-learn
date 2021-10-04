using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/competitions/[action]")]
    [ApiController]
    public class CompetitionController : Controller
    {
        public CompetitionController(ICompetitionService competitionService, ILoggerFactory loggerFactory)
        {
            _competitionService = competitionService;
            logger = loggerFactory.CreateLogger("FileLogger");
            logger.LogInformation("Entered {0} Controller", "Competition");
        }
        private readonly ICompetitionService _competitionService;
        private readonly ILogger logger;

        [HttpPost]
        public IActionResult Create([FromBody] CompetitionRequest item)
        {
            if (item == null)
            {
                logger.LogInformation("Failed at creating new Competition {0}", item.Name);
                return BadRequest();
            }
            var competition = _competitionService.Create(item);
            logger.LogInformation("Created Competition: {0}", competition.Id);
            return CreatedAtRoute("GetCompetition", new { id = competition.Id }, competition);
        }

        [HttpGet]
        public IEnumerable<Competition> GetAll()
        {
            logger.LogInformation("Got all competitions");
            return _competitionService.GetAll();
        }
        [HttpGet("{id}", Name = "GetCompetition")]
        public IActionResult GetById(int id)
        {
            var item = _competitionService.FindById(id);
            if (item == null)
            {
                logger.LogInformation("Failed at getting Competition {0}", id);
                return NotFound();
            }
            logger.LogInformation("Got by id Competition: {0}", id);
            return new ObjectResult(item);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _competitionService.FindById(id);
            if (item == null)
            {
                logger.LogInformation("Failed at deleting Competition {0}", id);
                return NotFound();
            }

            _competitionService.Delete(id);
            logger.LogInformation("Deleted Competition: {0}", id);
            return new NoContentResult();
        }
    }
}
