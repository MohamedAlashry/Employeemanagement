using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Repositories
{
    public class UnitOfWork : IGenericUnitOfWork
    {
        private readonly EmployeeDBContext _dbContext;

        private bool _disposed = false;

        public IGenericRepository<Employee> EmpRepo {get;}

        public IGenericRepository<EmployeeImages> EmpImagesRepo { get; }

        public UnitOfWork(EmployeeDBContext dbContext)
        {
            _dbContext = dbContext;
            EmpRepo = new GenericRepository<Employee>(_dbContext);
            EmpImagesRepo = new GenericRepository<EmployeeImages>(_dbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._dbContext.SaveChangesAsync() ;
        }

        private void _Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._dbContext.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            this._Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
