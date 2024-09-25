using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mvc.Project.DAL.Models;
using System.Reflection;


namespace Mvc.Project.DAL.Data
{                                                      
    public class ApplicationDbContext : /*DbContext*/ IdentityDbContext //=> (Contain (IdentitySecurtyWithDbSetFor(7)Table) and (inherited From DbContext) )
    {
        public ApplicationDbContext(DbContextOptions options): base(options){}

        //// Connect With DB
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //                => optionsBuilder.UseSqlServer("server=.; Database=MvcAppliction; Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //to implement configrtion for DbSetFor(7)Table  in IdentityDbContext
            base.OnModelCreating(modelBuilder); 

            // Add For All ModelConfigrations.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

       
        // Add Table In Sql Server ..
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; } 


    }
}
