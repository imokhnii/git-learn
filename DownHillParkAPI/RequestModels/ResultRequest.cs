using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.RequestModels
{
    public class ResultRequest
    {
        public int CompetitionId { get; set; }
        public string UserId { get; set; }
        public TimeSpan Time { get; set; }
    }
}
