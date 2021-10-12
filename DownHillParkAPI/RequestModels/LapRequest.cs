using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.RequestModels
{
    public class LapRequest
    {
        public int LapNumber { get; set; }
        public TimeSpan LapTime { get; set; }
    }
}
