using Microsoft.EntityFrameworkCore;
using Mvc.Project.BLL.Interfaces;
using Mvc.Project.DAL.Data;
using Mvc.Project.DAL.Models;
using System.Linq;


namespace Mvc.Project.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        //public IQueryable<Department> SearchByName(string name)
        //{
        //    return _dbContext.Departments.Where(e => e.Name.ToLower().Contains(name));
        //}
    }
}
