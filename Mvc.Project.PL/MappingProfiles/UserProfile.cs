using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mvc.Project.PL.ViewModels.Users;

namespace Mvc.Project.PL.MappingProfiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser,UserViewModel>().ReverseMap();
        }
    }
}
