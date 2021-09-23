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
    [Route("api/[controller]/[action]")]
    public class BikeController : ControllerBase
    {
        public BikeController(IBikeRepository bikesmanager)
        {
            _bikesManager = bikesmanager;
        }
        public IBikeRepository _bikesManager { get; set; }

        [HttpPost]
        public IActionResult Create([FromBody] BikeRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var bike = new Bike
            {
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Country = item.Country
            };
            _bikesManager.Add(bike);
            return CreatedAtRoute("GetItem", new { id = bike.Id }, bike);
        }

        [HttpGet]
        public IEnumerable<Bike> GetAll()
        {
            return _bikesManager.GetAll();
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = _bikesManager.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        public IActionResult GetByUser(User user)
        {
            var item = _bikesManager.FindByUser(user);
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

            var bike = _bikesManager.FindById(id);
            if (bike == null)
            {
                return NotFound();
            }

            _bikesManager.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Bike item, int id)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var bike = _bikesManager.FindById(id);
            if (bike == null)
            {
                return NotFound();
            }

            item.Id = bike.Id;

            _bikesManager.Update(item);
            return new NoContentResult();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _bikesManager.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            _bikesManager.Remove(id);
            return new NoContentResult();
        }
    }
}
