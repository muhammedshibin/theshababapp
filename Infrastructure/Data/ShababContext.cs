using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ShababContext : DbContext
    {
        public ShababContext(DbContextOptions<ShababContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Inmate> Inmates { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<TransactionDetail> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InmateBill> InmateBills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<BillPayment> Payments { get; set; }

    }
}
