using Core.Entities;
using Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class InmateConfig : IEntityTypeConfiguration<Inmate>
    {
        public void Configure(EntityTypeBuilder<Inmate> builder)
        {
            builder.HasMany(i => i.InmateLeaves).WithOne(l => l.Inmate).HasForeignKey(k => k.InmateId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(i => i.Status).HasConversion(o => o.ToString(), o => (InmateStatus)Enum.Parse(typeof(InmateStatus), o));
        }
    }
}
