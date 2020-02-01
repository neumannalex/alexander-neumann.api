using alexander_neumann.api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace alexander_neumann.api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TrainingRun> TrainingRuns { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public string GetNewId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public override int SaveChanges()
        {
            HandleAuditableEntities();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleAuditableEntities();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleAuditableEntities()
        {
            var entries = ChangeTracker
                            .Entries()
                            .Where(e => e.Entity is IAuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((IAuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((IAuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
