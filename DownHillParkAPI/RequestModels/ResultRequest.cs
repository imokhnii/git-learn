using System;
using System.Collections.Generic;

namespace DownHillParkAPI.RequestModels
{
    public class ResultRequest
    {
        public int CompetitionId { get; set; }
        public string UserId { get; set; }
        public TimeSpan Time { get; set; }
        public List<LapRequest> Laps { get; set; }
    }
}
