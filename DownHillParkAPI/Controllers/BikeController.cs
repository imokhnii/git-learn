using AutoMapper;
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
        public BikeController(IBikeRepository bikesmanager, IMapper mapper)
        {
            _bikesManager = bikesmanager;
            _mapper = mapper;
        }
        public IBikeRepository _bikesManager { get; set; }
        public IMapper _mapper;

        [HttpPost]
        public IActionResult Create([FromBody] BikeRequest item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var bike = _mapper.Map<BikeRequest,Bike>(item);
            _bikesManager.Add(bike);
            return CreatedAtRoute("GetBike", new { id = bike.Id }, bike);
        }

        [HttpGet]
        public IEnumerable<Bike> GetAll()
        {
            return _bikesManager.GetAll();
        }

        [HttpGet("{id}", Name = "GetBike")]
        public IActionResult GetById(int id)
        {
            var item = _bikesManager.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
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
