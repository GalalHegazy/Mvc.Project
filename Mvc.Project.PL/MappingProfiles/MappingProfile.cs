using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mvc.Project.DAL.Models;
using Mvc.Project.PL.ViewModels;
using Mvc.Project.PL.ViewModels.Roles;
using Mvc.Project.PL.ViewModels.Users;

namespace Mvc.Project.PL.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();

            CreateMap<IdentityUser, UserViewModel>().ReverseMap();

            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();

            CreateMap<EmployeeViewModel, Employee>().ReverseMap();

        }
    }
}
