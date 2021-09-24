using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfStart { get; set; } 
        public DateTime DateOfEnd { get; set; }
        public virtual ICollection<User> Participants { get; set; }
    }
}
