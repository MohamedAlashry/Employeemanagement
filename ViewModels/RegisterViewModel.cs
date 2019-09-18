using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote("IsEmailExist","Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Confirm password Must match the Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
    }
}
