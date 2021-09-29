using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CompetitionController(ICompetitionService competitionService)
        {
            _competitionService = competitionService;
        }
        private readonly ICompetitionService _competitionService;

        [HttpPost]
        public IActionResult Create([FromBody] CompetitionRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var competition = _competitionService.Create(item);
            return CreatedAtRoute("GetCompetition", new { id = competition.Id }, competition);
        }

        [HttpGet]
        public IEnumerable<Competition> GetAll()
        {
            return _competitionService.GetAll();
        }
        [HttpGet("{id}", Name = "GetCompetition")]
        public IActionResult GetById(int id)
        {
            var item = _competitionService.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _competitionService.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            _competitionService.Delete(id);
            return new NoContentResult();
        }
    }
}
