using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/teams/[action]")]
    public class TeamController : ControllerBase
    {
        public TeamController(ITeamRepository teams)
        {
            _teams = teams;
        }
        public ITeamRepository _teams { get; set; }

        [HttpPost]
        public IActionResult Create([FromBody] Team item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _teams.Add(item);
            return CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }
        
        [HttpGet]
        public IEnumerable<Team> GetAll()
        {
            return _teams.GetAll();
        }
        
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = _teams.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet]
        public IActionResult GetByName(string name)
        {
            var item = _teams.FindByName(name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _teams.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            _teams.Remove(id);
            return new NoContentResult();
        }
    }
}
