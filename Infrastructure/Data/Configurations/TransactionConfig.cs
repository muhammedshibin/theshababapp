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
    public class TransactionConfig : IEntityTypeConfiguration<TransactionDetail>
    {
        public void Configure(EntityTypeBuilder<TransactionDetail> builder)
        {
            builder.Property(t => t.Status).HasConversion(o => o.ToString(),t => (TransactionStatus)Enum.Parse(typeof(TransactionStatus), t));
        }
    }

    public class VendorConfig : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasMany(t => t.OutgoingTransactions).WithOne(v => v.PaidParty)
                .HasForeignKey(fk => fk.PaidPartyId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.IncomingTransactions).WithOne(v => v.PaidTo)
               .HasForeignKey(fk => fk.PaidToId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
