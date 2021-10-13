using System;
using System.Collections.Generic;

namespace DownHillParkAPI.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime DateOfStart { get; set; } 
        public DateTime DateOfEnd { get; set; }
        public int? CompetitionResultId { get; set; }
        public List<CompetitionResult> Results { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public int? CompetitionPrizeId { get; set; }
        public virtual CompetitionPrize Prize { get; set; }
    }
}
