using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ShababContext : DbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ShababContext(DbContextOptions<ShababContext> options,IHttpContextAccessor contextAccessor) : base(options)
        {
            _contextAccessor = contextAccessor;
        }

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
        public DbSet<DeactivatedInmate> DeactivatedInmates { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var userName = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name);

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModfiedOn = DateTime.UtcNow;
                ((BaseEntity)entityEntry.Entity).ModifiedBy = userName?.Value;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedOn = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).CreatedBy = userName?.Value;
                }
            }

            return await base.SaveChangesAsync(true, cancellationToken);
        }
    

}
}
