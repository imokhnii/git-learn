using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.RequestModels
{
    public class ChangePassword
    {
        public string Email { get; set; }
        public string CurrentPasword { get; set; }
        public string NewPassword { get; set; }
    }
}
