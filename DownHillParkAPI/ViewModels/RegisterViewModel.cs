using DownHillParkAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Year of birth")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Bike")]
        public Bike Bike { get; set; }

        [Required]
        [Display(Name = "Team")]
        public Team Team { get; set; }
    }
}
