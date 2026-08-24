using Microsoft.EntityFrameworkCore;
using rfq.api.Entities;

namespace rfq.api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<RfqPortalRfq> RfqPortalRfqs { get; set; }
    public DbSet<RfqPortalRfqItem> RfqPortalRfqItems { get; set; }
    public DbSet<RfqPortalMasterData> RfqPortalMasterData { get; set; }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<RfqPortalRfq>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
