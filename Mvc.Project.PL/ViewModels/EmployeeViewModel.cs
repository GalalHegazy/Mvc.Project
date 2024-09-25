using Mvc.Project.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Mvc.Project.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required ")] // Validtion For Clint Side In APP
        [MaxLength(50, ErrorMessage = "Max Length Of Name Is 50 Chars")] // Validtion For Server Side 
        [MinLength(5, ErrorMessage = "Min Length Of Name Is 5 Chars")]
        public string Name { get; set; }

        [Range(22,30,ErrorMessage ="Age Must Be In Range From  22 To 35")]  // Validtion For Clint Side In APP
        public int Age { get; set; }
        public Gander Gander { get; set; }

        [Phone] // Validtion For Clint Side In APP
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [EmailAddress] // Validtion For Clint Side In APP
        public string Email { get; set; }

       //[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", // Validtion For Clint Side In APP
       //    ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }
        [Display(Name = "Employee Type")]
        public EmpType EmployeeType { get; set; }

        [Display(Name = "Hiring Date")] // Validtion For Clint Side In APP
        public DateTime HiringDate { get; set; }

        [DataType(DataType.Currency)] // Validtion For Clint Side In APP
        public decimal Salary { get; set; }
        public IFormFile FormFile { get; set; } // Just for take file from View Model
        [Display(Name= "Image")]
        public string ImageName  { get; set; } // To Mapping file To dataBase
        public bool IsDeleted { get; set; } = false; // Value for Table and if you want to display it in clint side .

        [Display(Name = "Is Active")] // Validtion For Clint Side In APP
        public bool IsActive { get; set; }

        [InverseProperty("Employees")]
        public Department Department { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }   //  onDelete : Restrict
    }
}
