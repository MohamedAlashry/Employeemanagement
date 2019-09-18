using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IGenericUnitOfWork:IDisposable
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<Employee> EmpRepo { get; }
        IGenericRepository<EmployeeImages> EmpImagesRepo { get; }
    }
}
