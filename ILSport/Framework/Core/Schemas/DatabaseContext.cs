using Microsoft.EntityFrameworkCore;

namespace ILSport.Framework.Core.Schemas;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
    public DatabaseContext() {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ILSport.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable(nameof(Users), "test");
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(k => k.Id);
            e.HasIndex(i => i.Login).IsUnique();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}