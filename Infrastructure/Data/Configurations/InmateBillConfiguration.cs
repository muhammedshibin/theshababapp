using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class InmateBillConfiguration : IEntityTypeConfiguration<InmateBill>
    {
        public void Configure(EntityTypeBuilder<InmateBill> builder)
        {
            builder.HasMany(ib => ib.BillItems).WithOne(b => b.inmateBill)
                .HasForeignKey(k => k.InmateBillId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(ib => ib.BillAmount).HasColumnType("double(10,2)");
        }
    }    
}
