using System;

namespace DownHillParkAPI.RequestModels
{
    public class LapRequest
    {
        public int LapNumber { get; set; }
        public TimeSpan LapTime { get; set; }
    }
}
