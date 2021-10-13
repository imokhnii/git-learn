using System;
using System.Collections.Generic;

namespace DownHillParkAPI.Models
{
    public class CompetitionResult
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
        public string UserId { get; set; }
        public TimeSpan TotalTime { get; set; }
        public virtual List<Lap> Laps { get; set; }
    }
}
