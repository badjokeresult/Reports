using Microsoft.EntityFrameworkCore;

using Reports.Models.Entities;

namespace Reports.Models.ApplicationContext;

public class AppDbContext : DbContext
{
    public DbSet<Report> Reports { get; set; } = null!;
 
    public AppDbContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=report_project;Username=report_user;Password=r3P0rt$");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>(entity => entity.HasKey(r => r.Id));

        base.OnModelCreating(modelBuilder);
    }
}