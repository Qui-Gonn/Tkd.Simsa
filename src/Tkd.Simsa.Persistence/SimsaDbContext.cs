namespace Tkd.Simsa.Persistence;

using Microsoft.EntityFrameworkCore;

public class SimsaDbContext : DbContext
{
    public SimsaDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}