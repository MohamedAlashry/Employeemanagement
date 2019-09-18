using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Repositories
{
    public class GenericRepository<TEntities>: IGenericRepository<TEntities> where TEntities : class
    {
        private readonly EmployeeDBContext _dbContext;
        private readonly DbSet<TEntities> _entities;

        public GenericRepository(EmployeeDBContext dbContext)
        {
            _dbContext = dbContext;
            _entities =_dbContext.Set<TEntities>();
        }

        public TEntities Add(TEntities entity)
        {
            _entities.Add(entity);
            return entity;
        }

        public TEntities Delete(int id)
        {
            TEntities entity = _entities.Find(id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
            return entity;
        }      

        public IEnumerable<TEntities> GetAll()
        {
            return _entities;
        }

        public TEntities GetById(int id)
        {
            return _entities.Find(id);
        }

        public TEntities Update(TEntities entity)
        {

            var attached = _entities.Attach(entity);
            attached.State = EntityState.Modified;
            return entity;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }


    }
}
