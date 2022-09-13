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
    public class SampleTableConfiguration : IEntityTypeConfiguration<SampleTable>
    {
        public void Configure(EntityTypeBuilder<SampleTable> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
