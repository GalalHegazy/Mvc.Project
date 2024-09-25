using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mvc.Project.PL.ViewModels.Roles;

namespace Mvc.Project.PL.MappingProfiles
{
    public class RoleProfile :Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleViewModel>().ReverseMap();
        }
    }
}
