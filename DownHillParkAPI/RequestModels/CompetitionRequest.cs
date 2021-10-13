using System;

namespace DownHillParkAPI.RequestModels
{
    public class CompetitionRequest
    {
        public string Name { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public string Type { get; set; }
    }
}
