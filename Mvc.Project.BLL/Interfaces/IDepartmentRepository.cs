using Mvc.Project.DAL.Models;

namespace Mvc.Project.BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        //You Can own Method for DepartmentRepo Only 
        //IQueryable<Department> SearchByName(string name);
    }
}
