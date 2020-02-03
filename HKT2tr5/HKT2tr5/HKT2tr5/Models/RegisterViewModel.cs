using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Models
{
    public class RegisterViewModel
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",
            ErrorMessage = "Pass and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
