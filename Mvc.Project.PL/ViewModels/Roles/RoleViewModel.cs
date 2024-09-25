using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Project.PL.ViewModels.Roles
{
    public class RoleViewModel
    {
        public string Id { get; set; } 
        [Display(Name ="Role Name")]
        public string Name { get; set; }
        public RoleViewModel()
        {
            Id= Guid.NewGuid().ToString();
        }
    }
}
