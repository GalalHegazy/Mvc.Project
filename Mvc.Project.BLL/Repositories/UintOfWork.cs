using Mvc.Project.BLL.Interfaces;
using Mvc.Project.DAL.Data;
using Mvc.Project.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Project.BLL.Repositories
{
    public class UintOfWork : IUintOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }

        private Hashtable _repositories;

        public UintOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            DepartmentRepository= new DepartmentRepository(dbContext);
            EmployeeRepository= new EmployeeRepository(dbContext);  
            _repositories = new Hashtable();
        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ModelBase
        {
            var Key = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(Key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);

                _repositories.Add(Key, repository); 
            }

            return _repositories[Key] as IGenericRepository<TEntity>;
        }
    }
}
