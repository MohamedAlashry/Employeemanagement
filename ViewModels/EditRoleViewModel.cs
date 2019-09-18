using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<ApplicationUser>();
        }
        public string RoleId { get; set; }

        [Required]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }

        public List<ApplicationUser> Users { get; set; }

    }
}
