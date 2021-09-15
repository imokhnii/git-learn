using DownHillParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.ViewModels
{
    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public Bike Bike { get; set; }
        public Team Team { get; set; }

    }
}
