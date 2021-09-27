using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.RequestModels
{
    public class CompetitionPrizeRequest
    {
        public string firstPlace { get; set; }
        public string secondPlace { get; set; }
        public string thirdPlace { get; set; }
    }
}
