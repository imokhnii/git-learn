using DownHillParkAPI.Models;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        public async Task<IActionResult> Create([FromBody] BikeRequest item)
        {
            if (item == null)
            {
                logger.LogInformation("Failed at creating new Bike {0}", item.Manufacturer + ' ' + item.Model);
                return BadRequest();
            }
            var bike = await _bikeService.CreateAsync(item);
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
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _bikeService.FindByIdAsync(id);
            if (item == null)
            {
                logger.LogInformation("Failed at getting Bike {0}", id);
                return NotFound();
            }
            logger.LogInformation("Got by id Bike: {0}", id);
            return Ok(item);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _bikeService.FindByIdAsync(id);
            if (item == null)
            {
                logger.LogInformation("Failed at deleting Bike {0}", id);
                return NotFound();
            }

            await _bikeService.DeleteAsync(id);
            logger.LogInformation("Deleted Bike: {0}", id);
            return new NoContentResult();
        }
    }
}
