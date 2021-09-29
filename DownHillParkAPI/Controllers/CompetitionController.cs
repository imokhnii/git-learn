﻿using AutoMapper;
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
    [Route("api/competitions/[action]")]
    [ApiController]
    public class CompetitionController : Controller
    {
        public CompetitionController(ICompetitionRepository competitionManager, IMapper mapper)
        {
            _competitionManager = competitionManager;
            _mapper = mapper;
        }
        public ICompetitionRepository _competitionManager { get; set; }
        public IMapper _mapper;

        [HttpPost]
        public IActionResult Create([FromBody] CompetitionRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var competition = _mapper.Map<CompetitionRequest, Competition>(item);
            _competitionManager.Add(competition);
            return CreatedAtRoute("GetCompetition", new { id = competition.Id }, competition);
        }

        [HttpGet]
        public IEnumerable<Competition> GetAll()
        {
            return _competitionManager.GetAll();
        }
        [HttpGet("{id}", Name = "GetCompetition")]
        public IActionResult GetById(int id)
        {
            var item = _competitionManager.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _competitionManager.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            _competitionManager.Remove(id);
            return new NoContentResult();
        }
    }
}
