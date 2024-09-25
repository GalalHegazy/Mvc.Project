using Mvc.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Project.BLL.Interfaces
{
    public interface IUintOfWork : /*IDisposable*/ IAsyncDisposable
    {
       public IDepartmentRepository DepartmentRepository { get; set; }
       public IEmployeeRepository EmployeeRepository { get; set; }

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ModelBase; 
       Task<int> CompleteAsync();
    }
}
