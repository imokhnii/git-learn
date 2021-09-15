using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/bikes/[action]")]
    public class BikeController : ControllerBase
    {
        public BikeController(IBikeRepository bikes)
        {
            Bikes = bikes;
        }
        public IBikeRepository Bikes { get; set; }
        
        [HttpPost]
        public IActionResult Create([FromBody] Bike item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Bikes.Add(item);
            return CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }

        [HttpGet]
        public IEnumerable<Bike> GetAll()
        {
            return Bikes.GetAll();
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = Bikes.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        public IActionResult GetByUser(User user)
        {
            var item = Bikes.FindByUser(user);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Bike item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var bike = Bikes.FindById(id);
            if (bike == null)
            {
                return NotFound();
            }

            Bikes.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Bike item, int id)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var bike = Bikes.FindById(id);
            if (bike == null)
            {
                return NotFound();
            }

            item.Id = bike.Id;

            Bikes.Update(item);
            return new NoContentResult();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = Bikes.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            Bikes.Remove(id);
            return new NoContentResult();
        }
    }
}
