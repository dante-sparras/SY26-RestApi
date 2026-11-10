using AdsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdsApi.Data;

public class AdsDbContext(DbContextOptions<AdsDbContext> options) : DbContext(options)
{
    public DbSet<Ad> Ads => Set<Ad>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ad>(entity =>
        {
            entity.Property(a => a.Title).HasMaxLength(100).IsRequired();
            entity.Property(a => a.Description).HasMaxLength(1000).IsRequired();
            entity.Property(a => a.Price).HasPrecision(18, 2);
        });
    }
}
