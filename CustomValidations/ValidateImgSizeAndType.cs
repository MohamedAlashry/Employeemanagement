using EmployeeManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.CustomValidations
{
    public class ValidateImgSizeAndType:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as EmployeeViewModel;
            if (model.Files!=null&&model.Files.Any())
            {
                foreach (var file in model.Files)
                {
                    if (file.Length>1048576)
                    {
                        return new ValidationResult("Image size not more the 1 MBs!");
                    }
                    if (String.Compare(file.ContentType.Split('/')[1],"jpg",StringComparison.OrdinalIgnoreCase)!=0 &&
                        String.Compare(file.ContentType.Split('/')[1],"png", StringComparison.OrdinalIgnoreCase) != 0&&
                        String.Compare(file.ContentType.Split('/')[1], "jpeg", StringComparison.OrdinalIgnoreCase) != 0 )
                    {
                        return new ValidationResult("Image type must be .jpg,.png!");

                    }
                }
            }
            return ValidationResult.Success;

        }
       
    }
}
