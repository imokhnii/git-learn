﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Models
{
    public class Lap
    {
        public int Id { get; set; }
        public int ResultId { get; set; }
        public virtual CompetitionResult Result { get; set; }
        public int LapNumber { get; set; }
        public string UserId { get; set; }
        public TimeSpan LapTime { get; set; }
    }
}
