using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Controllers
{
    [Route("api/users/[action]")]
    public class UserController : ControllerBase
    {
        public UserController(IUserRepository users)
        {
            Users = users;
        }
        public IUserRepository Users { get; set; }
        
        [HttpPost]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Users.Add(item);
            return CreatedAtRoute("GetItem", new { id = item.Id }, item);
        }
        
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return Users.GetAll();
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = Users.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        public IActionResult GetByFullName(string firstname, string lastname)
        {
            var item = Users.FindByFullName(firstname,lastname);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        [HttpGet]
        public IActionResult GetByBike(Bike bike)
        {
            var item = Users.FindByBike(bike);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var user = Users.FindById(id);
            if (user == null)
            {
                return NotFound();
            }

            Users.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] User item, int id)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var user = Users.FindById(id);
            if (user == null)
            {
                return NotFound();
            }

            item.Id = user.Id;

            Users.Update(item);
            return new NoContentResult();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = Users.FindById(id);
            if (item == null)
            {
                return NotFound();
            }

            Users.Remove(id);
            return new NoContentResult();
        }
    }
}
