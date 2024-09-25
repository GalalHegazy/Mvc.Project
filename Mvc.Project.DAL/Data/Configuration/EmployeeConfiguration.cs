using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mvc.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Project.DAL.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {  
                                          // (Validation in server side ...)
             
            builder.Property(E=>E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(E=>E.Address).IsRequired();
            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");

            builder.Property(E => E.Gander)
                   .HasConversion(
                      (Gander) => Gander.ToString(),  // To Set In DB as string .
                      (GanderAsString) => (Gander) Enum.Parse(typeof(Gander),GanderAsString,true) // Get From DB As a Gander Type.
                    );

            builder.Property(E => E.EmployeeType)
                  .HasConversion(
                     (EmployeeType) => EmployeeType.ToString(),  // To Set In DB as string .
                     (EmployeeTypeAsString) => (EmpType) Enum.Parse(typeof(EmpType), EmployeeTypeAsString, true) // Get From DB As a EmpType Type.
                   );


        }
    }
}
