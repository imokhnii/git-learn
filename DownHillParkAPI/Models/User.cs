using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Bike> Bikes { get; set; }
        public virtual Team Team { get; set; }
        public int? TeamId { get; set; }
        public string Role { get; set; }

    }
}
