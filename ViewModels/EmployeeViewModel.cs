using EmployeeManagement.CommonHelpers;
using EmployeeManagement.CustomValidations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name Field Is Required")]
        [MaxLength(50,ErrorMessage ="Name field must be less than 50 characters")]
        public string Name { get; set; }       

        [Required(ErrorMessage = "Eamil Field Is Required")]
        [DataType(DataType.EmailAddress,ErrorMessage ="nvalid Email Type")]
        [Display(Name ="Office Email")]
        [MaxLength(100)]
        public string Email { get;  set; }

        [Required(ErrorMessage = "Department Field Is Required")]
        public int DeptId { get; set; }

        [ValidateImgSizeAndType]
        [Display(Name = "Employee image")]
        public List<IFormFile> Files { get; set; }

        public string PageTitle { get; set; }

        public List<string> Photos { get; set; }
    }
}
