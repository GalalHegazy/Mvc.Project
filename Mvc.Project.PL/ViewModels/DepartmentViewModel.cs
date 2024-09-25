using Mvc.Project.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mvc.Project.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Code Is Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Department Name Is Required")]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]  // edit for display column name in view * must be write in view nodel *
        public DateTime DateOfCreation { get; set; }

    }
}
