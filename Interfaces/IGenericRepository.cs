using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Interfaces
{
    public interface IGenericRepository<TEntities>: IDisposable where TEntities : class
    {
        TEntities GetById(int id);
        IEnumerable<TEntities> GetAll();
        TEntities Delete(int id);
        TEntities Update(TEntities entity);
        TEntities Add(TEntities t);


    }
}
