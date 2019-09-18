using EmployeeManagement.CommonHelpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
   
    public class EmployeeDBContext:IdentityDbContext<ApplicationUser>
    {   
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options):base(options)
        {

        }
    
        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeImages> EmployeeImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
