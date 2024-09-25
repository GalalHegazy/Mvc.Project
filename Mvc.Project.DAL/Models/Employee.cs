using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace Mvc.Project.DAL.Models
{
    public enum Gander
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }
    public enum EmpType
    {
        [EnumMember(Value = "FullTime")]
        FullTime = 1,
        [EnumMember(Value = "PartTime")]
        PartTime = 2
    }
    public class Employee :ModelBase
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)] // Validtion For server side
        public string Name { get; set; }
        public int Age { get; set; }
        public Gander Gander { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }   
        public EmpType EmployeeType { get; set; }
        public DateTime HiringDate { get; set; }
        public decimal Salary { get; set; }
        public bool IsDeleted { get; set; } = false; // Value for Table and if you want to display it in clint side .
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now; // Value for Table and if you want to display it in clint side .

        [InverseProperty("Employees")]
        public Department Department { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }   //  onDelete : Restrict

    }
}
