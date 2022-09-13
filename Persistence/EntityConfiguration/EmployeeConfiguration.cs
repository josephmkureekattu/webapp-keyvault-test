using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employeecs>
    {
        public void Configure(EntityTypeBuilder<Employeecs> builder)
        {
            builder.HasOne(x => x.Department).WithMany(m => m.Employees).HasForeignKey(x => x.DepartmentId);

        }
    }
}
