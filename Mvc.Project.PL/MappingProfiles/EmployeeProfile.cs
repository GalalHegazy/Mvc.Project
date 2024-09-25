using AutoMapper;
using Mvc.Project.DAL.Models;
using Mvc.Project.PL.ViewModels;

namespace Mvc.Project.PL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
