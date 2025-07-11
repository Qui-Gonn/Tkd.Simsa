namespace Tkd.Simsa.Persistence.Test.Helper;

using Microsoft.EntityFrameworkCore;

internal class TestDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TestEntity> TestEntities { get; set; }
}