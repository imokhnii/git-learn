using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI
{
    public class RegisterFailedException : Exception
    {
        public RegisterFailedException() : base()
        {
        }
        public RegisterFailedException(string message) : base(message)
        {
        }
        public RegisterFailedException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
