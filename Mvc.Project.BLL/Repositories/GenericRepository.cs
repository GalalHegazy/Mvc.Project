using Microsoft.EntityFrameworkCore;
using Mvc.Project.BLL.Interfaces;
using Mvc.Project.BLL.Repositories.Specifications;
using Mvc.Project.DAL.Data;
using Mvc.Project.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Mvc.Project.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase 
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Add(entity); // Talk With DbSet<T> <Depaetment,Employee>
           /* _dbContext.Set<T>().Add(entity); *///or 
            //return _dbContext.SaveChanges(); (Go To UnitOfWork)
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity); // Talk With DbSet<T> <Depaetment,Employee>
            /* _dbContext.Set<T>().Update(entity); *///or 
            //return _dbContext.SaveChanges(); (Go To UnitOfWork)

        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity); // Talk With DbSet<T> <Depaetment,Employee>
            /* _dbContext.Set<T>().Remove(entity); *///or 
            //return _dbContext.SaveChanges(); (Go To UnitOfWork)

        }

        public async Task<IEnumerable<T>> GetAllAsync() //حل مؤقت لل eager louding  // علشان loud Department
        {
            //if (typeof(T) == typeof(Employee))
            //{
            //    return (IEnumerable<T>)await _dbContext.Employees.Include(e => e.Department).AsNoTracking().ToListAsync();
            //}

            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
                        => await _dbContext.FindAsync<T>(id);   /* _dbContext.Set<T>().Find<T>(id); *///or 

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).AsNoTracking().ToListAsync();
        }
        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).FirstOrDefaultAsync();

        }
        public IQueryable<T> SearchByName(ISpecifications<T> spec)
        {
            //return _dbContext.Employees.Where(e => e.Name.ToLower().Contains(name))
            //                            .Include(e => e.Department).AsNoTracking();
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(),spec).AsNoTracking();
        }
    }
}
