namespace Tkd.Simsa.Persistence.Test.Helper;

using AutoBogus;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

internal static class InMemorySqliteDbHelper
{
    public static InMemorySqliteDbHelper<TDbContext> Create<TDbContext>()
        where TDbContext : DbContext
        => new ();
}

internal class InMemorySqliteDbHelper<TDbContext> : IDisposable
    where TDbContext : DbContext
{
    private PooledDbContextFactory<TDbContext>? dbContextFactory;

    private DbContextOptions<TDbContext>? dbContextOptions;

    private SqliteConnection? sqliteConnection;

    public TDbContext CreateDbContext()
    {
        if (this.sqliteConnection is null)
        {
            this.sqliteConnection = new SqliteConnection("Filename=:memory:");
            this.sqliteConnection.Open();
        }

        this.dbContextOptions ??= new DbContextOptionsBuilder<TDbContext>()
            .UseSqlite(this.sqliteConnection)
            .Options;
        this.dbContextFactory ??= new PooledDbContextFactory<TDbContext>(this.dbContextOptions);
        var dbContext = this.dbContextFactory.CreateDbContext();
        dbContext.Database.EnsureCreated();
        return dbContext;
    }

    public void Dispose()
        => this.sqliteConnection?.Dispose();

    public List<TEntity> GenerateFakeData<TEntity>(int count)
        where TEntity : class
    {
        var autoFaker = new AutoFaker<TEntity>();
        var generatedItems = autoFaker.Generate(count);
        using var dbContext = this.CreateDbContext();
        dbContext.AddRange(generatedItems);
        dbContext.SaveChanges();
        return generatedItems;
    }
}