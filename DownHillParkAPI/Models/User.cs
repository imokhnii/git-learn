using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string Country { get; set; }
        public Bike Bike { get; set; }
        public Team Team { get; set; }
        public string Role { get; set; }


    }
}
