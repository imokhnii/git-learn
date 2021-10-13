using System.Collections.Generic;

namespace DownHillParkAPI.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
