using AdsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdsApi.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AdsDbContext>();

        await db.Database.MigrateAsync();

        if (await db.Ads.AnyAsync())
        {
            return;
        }

        db.Ads.AddRange(
            new Ad
            {
                Title = "Mountain bike",
                Description = "Used mountain bike in good condition.",
                Price = 2500m
            },
            new Ad
            {
                Title = "Office chair",
                Description = "Ergonomic office chair, barely used.",
                Price = 750m
            },
            new Ad
            {
                Title = "Gaming console",
                Description = "Console with two controllers and three games.",
                Price = 3000m
            });

        await db.SaveChangesAsync();
    }
}
