using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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
        public virtual Competition CurrentCompetition { get; set; }
        public int? CompetitionId { get; set; }
        public int? TeamId { get; set; }

    }
}
