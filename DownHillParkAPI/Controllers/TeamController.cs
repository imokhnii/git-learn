using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
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
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        private readonly ITeamService _teamService;

        [HttpPost]
        public IActionResult Create([FromBody] TeamRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var team = _teamService.Create(item);
            return CreatedAtRoute("GetTeam", new { id = team.Id }, team);
        }

        [HttpGet]
        public IEnumerable<Team> GetAll()
        {
            return _teamService.GetAll();
        }

        [HttpGet("{id}", Name = "GetTeam")]
        public IActionResult GetById(int id)
        {
            var item = _teamService.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _teamService.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            _teamService.Delete(id);
            return new NoContentResult();
        }
    }
}
