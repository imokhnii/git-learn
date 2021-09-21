using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
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
        public CompetitionController(ICompetitionRepository competitions)
        {
            Competitions = competitions;
        }
        public ICompetitionRepository Competitions { get; set; }
        
        [HttpPost]
        public IActionResult Create([FromBody] Competition item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Competitions.Create(item);
            return CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }

        [HttpGet]
        public IEnumerable<Competition> GetAll()
        {
            return Competitions.GetAll();
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = Competitions.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet]
        public IActionResult GetByName(string name)
        {
            var item = Competitions.FindByName(name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Competition item, int id)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var competition = Competitions.FindById(id);
            if (competition == null)
            {
                return NotFound();
            }

            item.Id = competition.Id;

            Competitions.Update(item);
            return new NoContentResult();
        }
        [HttpPatch]
        public IActionResult AddParticipant(int id, string UserId)
        {
            var competition = Competitions.FindById(id);
            if (competition == null)
            {
                return NotFound();
            }
            Competitions.AddParticipant(id, UserId);
            return new NoContentResult();
        }
        [HttpPatch]
        public IActionResult RemoveParticipant(int id, string UserId)
        {
            var competition = Competitions.FindById(id);
            if(competition == null)
            {
                return NotFound();
            }
            Competitions.RemoveParticipant(id, UserId);
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = Competitions.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            Competitions.Remove(id);
            return new NoContentResult();
        }
    }
}
