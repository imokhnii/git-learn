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
            Teams = teams;
        }
        public ITeamRepository Teams { get; set; }

        [HttpPost]
        public IActionResult Create([FromBody] Team item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Teams.Add(item);
            return CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }
        
        [HttpGet]
        public IEnumerable<Team> GetAll()
        {
            return Teams.GetAll();
        }
        
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = Teams.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpGet]
        public IActionResult GetByName(string name)
        {
            var item = Teams.FindByName(name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = Teams.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            Teams.Remove(id);
            return new NoContentResult();
        }
    }
}
