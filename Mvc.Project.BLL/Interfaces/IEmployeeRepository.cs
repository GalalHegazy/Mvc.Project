using Mvc.Project.DAL.Models;
using System.Collections.Generic;
using System.Linq;


namespace Mvc.Project.BLL.Interfaces
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        //You Can own Method for EmpployeeRepo Only 

        //IQueryable<Employee> GetEmployeeByAddress(string address);
        //IQueryable<Employee> SearchByName(string name);

    }
}
