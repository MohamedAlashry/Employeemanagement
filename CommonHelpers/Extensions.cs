using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.CommonHelpers
{
    public static class Extensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Department = 1,
                   Email = "hamada@yahoo.com",
                   Name = "hamda"
               },
                 new Employee
                 {
                     Id = 3,
                     Department = 3,
                     Email = "mai@yahoo.com",
                     Name = "mai"
                 }
                
               );   

        }
    }
}
