using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeImages
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ImageId { get; set; }

        [ForeignKey("Owner")]
        public int EmpId { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        public Employee Owner { get; set; }
    }
}
