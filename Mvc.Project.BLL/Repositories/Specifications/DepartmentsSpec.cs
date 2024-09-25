using Mvc.Project.DAL.Models;

namespace Mvc.Project.BLL.Repositories.Specifications
{
    public  class DepartmentsSpec : BaseSpecifications<Department>
    {
        public DepartmentsSpec(string name) : base(e => e.Name.ToLower().Contains(name))
        {
        }
    }
}
