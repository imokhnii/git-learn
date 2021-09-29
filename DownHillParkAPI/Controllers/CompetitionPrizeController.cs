using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CompetitionPrizeController(ICompetitionPrizeRepository prizeManager, ICompetitionRepository competitionManager, IMapper mapper)
        {
            _prizeManager = prizeManager;
            _competitionManager = competitionManager;
            _mapper = mapper;
        }
        public ICompetitionPrizeRepository _prizeManager;
        public ICompetitionRepository _competitionManager;
        public IMapper _mapper;

        [HttpPost]
        public IActionResult Create([FromBody] CompetitionPrizeRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var prize = _mapper.Map<CompetitionPrizeRequest, CompetitionPrize>(item);
              //{
              //    firstPlace = item.firstPlace,
              //    secondPlace = item.secondPlace,
              //    thirdPlace = item.thirdPlace

              //};
              _prizeManager.Add(prize);
            return CreatedAtRoute("GetCompetitionPrize", new { id = prize.Id }, prize);
        }

        [HttpPost]
        public IActionResult AddPrizesToCompetition(int CompetitionId, int PrizeId)
        {
            if (ModelState.IsValid)
            {
                var competition = _competitionManager.FindById(CompetitionId);
                var prize = _prizeManager.FindById(PrizeId);
                if (competition != null && prize != null)
                {
                    prize.CompetitionId = CompetitionId;
                    competition.CompetitionPrizeId = PrizeId;
                    _prizeManager.Update(prize);
                    return Ok(prize);
                }
            }
            return NotFound();
        }

        [HttpGet("{id}", Name = "GetCompetitionPrize")]
        public IActionResult GetById(int id)
        {
            var item = _prizeManager.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _prizeManager.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            _prizeManager.Remove(id);
            return new NoContentResult();
        }
    }
}
