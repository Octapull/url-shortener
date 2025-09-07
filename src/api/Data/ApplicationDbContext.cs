using api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {}
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    public DbSet<UrlClickStat> UrlClickStats { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ShortenedUrl>()
            .HasIndex(u => u.Code)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => new { u.Provider, u.ProviderId })
            .IsUnique();
        
        modelBuilder.Entity<UrlClickStat>()
            .HasOne<ShortenedUrl>()
            .WithMany()
            .HasForeignKey(c => c.ShortenedUrlId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}