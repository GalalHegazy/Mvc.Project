using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mvc.Project.DAL.Models;

namespace Mvc.Project.DAL.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(x=>x.Id).UseIdentityColumn(10,10);
            builder.Property(x => x.Code).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        }
    }


}
