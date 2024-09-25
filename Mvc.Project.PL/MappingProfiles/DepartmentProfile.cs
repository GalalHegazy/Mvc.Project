using AutoMapper;
using Mvc.Project.DAL.Models;
using Mvc.Project.PL.ViewModels;

namespace Mvc.Project.PL.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
           CreateMap<DepartmentViewModel,Department>().ReverseMap();
            
        }
    }
}
