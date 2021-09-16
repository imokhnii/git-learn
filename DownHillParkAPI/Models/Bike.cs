using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
