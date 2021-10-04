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
        public IActionResult Create([FromBody] CompetitionPrizeRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var prize = _prizeService.Create(item);
            logger.LogInformation("Created Prize: {0}", prize.Id);
            return CreatedAtRoute("GetCompetitionPrize", new { id = prize.Id }, prize);
        }

        [HttpPost]
        public IActionResult AddPrizesToCompetition(int CompetitionId, int PrizeId)
        {
            if (ModelState.IsValid)
            {
                var prize = _prizeService.AddPrizesToCompetition(CompetitionId, PrizeId);
                logger.LogInformation("Prize {0} added to Competition {1}", PrizeId, CompetitionId);
                if (prize != null)
                {
                    return Ok(prize);
                }
            }
            return NotFound();
        }

        [HttpGet("{id}", Name = "GetCompetitionPrize")]
        public IActionResult GetById(int id)
        {
            var item = _prizeService.FindById(id);
            logger.LogInformation("Got by id Prize{0}", id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _prizeService.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            _prizeService.Delete(id);
            logger.LogInformation("Deleted Prize {0}", id);
            return new NoContentResult();
        }
    }
}
