using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniTwit.Infrastructure.Entities;

namespace MiniTwit.Infrastructure.Data;

// Inherit from IdentityDBContex to enable EF Core Identity (Login functionality)
public class MiniTwitDBContext : IdentityDbContext<Author>
{
    public MiniTwitDBContext(DbContextOptions<MiniTwitDBContext> options)
        : base(options)
    {
        //empty on purpose
    }

    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the relationship between Cheep and Author
        modelBuilder.Entity<Cheep>().HasIndex(c => c.Date);
    }
}
