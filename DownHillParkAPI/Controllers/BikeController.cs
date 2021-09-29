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
    [Route("api/[controller]/[action]")]
    public class BikeController : ControllerBase
    {
        public BikeController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }
        private readonly IBikeService _bikeService;

        [HttpPost]
        public IActionResult Create([FromBody] BikeRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var bike = _bikeService.Create(item);
            return CreatedAtRoute("GetBike", new { id = bike.Id }, bike);
        }

        [HttpGet]
        public IEnumerable<Bike> GetAll()
        {
            return _bikeService.GetAll();
        }

        [HttpGet("{id}", Name = "GetBike")]
        public IActionResult GetById(int id)
        {
            var item = _bikeService.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _bikeService.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            _bikeService.Delete(id);
            return new NoContentResult();
        }
    }
}
