namespace Tkd.Simsa.Persistence;

using Microsoft.Extensions.DependencyInjection;

public static class DbContextInitializer
{
    public static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<SimsaDbContext>();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }
}