using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/_bikes/[action]")]
    public class BikeController : ControllerBase
    {
        public BikeController(IBikeRepository bikes)
        {
            _bikes = bikes;
        }
        public IBikeRepository _bikes { get; set; }
        
        [HttpPost]
        public IActionResult Create([FromBody] Bike item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _bikes.Add(item);
            return CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }

        [HttpGet]
        public IEnumerable<Bike> GetAll()
        {
            return _bikes.GetAll();
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = _bikes.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        public IActionResult GetByUser(User user)
        {
            var item = _bikes.FindByUser(user);
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

            var bike = _bikes.FindById(id);
            if (bike == null)
            {
                return NotFound();
            }

            _bikes.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Bike item, int id)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var bike = _bikes.FindById(id);
            if (bike == null)
            {
                return NotFound();
            }

            item.Id = bike.Id;

            _bikes.Update(item);
            return new NoContentResult();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _bikes.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            _bikes.Remove(id);
            return new NoContentResult();
        }
    }
}
