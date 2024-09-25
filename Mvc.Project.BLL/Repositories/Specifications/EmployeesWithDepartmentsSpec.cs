using Mvc.Project.DAL.Models;

namespace Mvc.Project.BLL.Repositories.Specifications
{
    public class EmployeesWithDepartmentsSpec : BaseSpecifications<Employee>
    {
        public EmployeesWithDepartmentsSpec()
        {
            Includes.Add(D => D.Department);
        }

        public EmployeesWithDepartmentsSpec(int? id) : base(E => E.Id == id)
        {
            Includes.Add(D => D.Department);
        }
        public EmployeesWithDepartmentsSpec(string name) : base(e => e.Name.ToLower().Contains(name))
        {
            Includes.Add(D => D.Department);
        }
    }
}
