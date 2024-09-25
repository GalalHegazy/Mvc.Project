using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mvc.Project.DAL.Models
{
    public class Department :ModelBase
    {
        public int Id { get; set; }
        public string Code { get; set; } 
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }  ///*************** How To set DateTime Now ??
        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
