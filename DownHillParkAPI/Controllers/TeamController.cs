using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
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
        public TeamController(ITeamRepository teamManager)
        {
            _teamManager = teamManager;
        }
        public ITeamRepository _teamManager { get; set; }

        [HttpPost]
        public IActionResult Create([FromBody] TeamRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var team = new Team
            {
                Name = item.Name,
                Country = item.Country
            };
            _teamManager.Add(team);
            return CreatedAtRoute("GetTeam", new { id = team.Id }, team);
        }

        [HttpGet]
        public IEnumerable<Team> GetAll()
        {
            return _teamManager.GetAll();
        }

        [HttpGet("{id}", Name = "GetTeam")]
        public IActionResult GetById(int id)
        {
            var item = _teamManager.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _teamManager.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            _teamManager.Remove(id);
            return new NoContentResult();
        }
    }
}
