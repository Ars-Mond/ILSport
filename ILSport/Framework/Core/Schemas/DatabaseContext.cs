using Microsoft.EntityFrameworkCore;

namespace ILSport.Framework.Core.Schemas;

public class DatabaseContext : DbContext
{
    public DbSet<UserSchema> Users { get; set; } = null!;
    public DbSet<TrainingGroupSchema> TrainingGroups { get; set; } = null!;
    public DbSet<TrainingSchema> Trainings { get; set; } = null!;
    public DbSet<UserTrainingSchema> UserTrainings { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
    public DatabaseContext() {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ILSport.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserSchema>(e =>
        {
            e.ToTable(nameof(Users), "test");
            e.HasKey(k => k.Id);
            e.HasIndex(i => i.Login).IsUnique();
            
            e.HasMany(r => r.UserTrainings)
                .WithOne(r2 => r2.User)
                .HasForeignKey(r3 => r3.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<TrainingGroupSchema>(e =>
        {
            e.ToTable(nameof(TrainingGroups), "test");
            e.HasKey(k => k.Id);
            e.HasIndex(i => i.NameIndex).IsUnique();

            e.HasMany(r => r.Trainings)
                .WithOne(r2 => r2.TrainingGroup)
                .HasForeignKey(r3 => r3.TrainingGroupId)
                .IsRequired();
        });
        
        modelBuilder.Entity<TrainingSchema>(e =>
        {
            e.ToTable(nameof(Trainings), "test");
            e.HasKey(k => k.Id);
            e.HasIndex(i => i.NameIndex).IsUnique();
            
            e.HasOne(r => r.TrainingGroup)
                .WithMany(r2 => r2.Trainings)
                .HasForeignKey(r3 => r3.TrainingGroupId)
                .IsRequired();
            
            e.HasMany(r => r.UserTrainings)
                .WithOne(r2 => r2.Training)
                .HasForeignKey(r3 => r3.TrainingId)
                .IsRequired();
        });

        modelBuilder.Entity<UserTrainingSchema>(e =>
        {
            e.ToTable(nameof(UserTrainings), "test");
            e.HasKey(k => k.Id);
            e.HasOne(r => r.User)
                .WithMany(r2 => r2.UserTrainings)
                .HasForeignKey(r3 => r3.UserId)
                .IsRequired();
            e.HasOne(r => r.Training)
                .WithMany(r2 => r2.UserTrainings)
                .HasForeignKey(r3 => r3.TrainingId)
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}