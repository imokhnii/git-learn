using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BikeController : ControllerBase
    {
        public BikeController(IBikeService bikeService, ILoggerFactory loggerFactory)
        {
            _bikeService = bikeService;
            logger = loggerFactory.CreateLogger("FileLogger");
            logger.LogInformation("Entered {0} Controller", "Bike");
        }
        private readonly IBikeService _bikeService;
        private readonly ILogger logger;

        [HttpPost]
        public IActionResult Create([FromBody] BikeRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var bike = _bikeService.Create(item);
            logger.LogInformation("Created Bike: {0}", bike.Id);
            return CreatedAtRoute("GetBike", new { id = bike.Id }, bike);
        }

        [HttpGet]
        public IEnumerable<Bike> GetAll()
        {
            logger.LogInformation("Got all bikes");
            return _bikeService.GetAll();
        }

        [HttpGet("{id}", Name = "GetBike")]
        public IActionResult GetById(int id)
        {
            var item = _bikeService.FindById(id);
            logger.LogInformation("Got by id Bike: {0}", id);
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
            logger.LogInformation("Deleted Bike: {0}", id);
            return new NoContentResult();
        }
    }
}
