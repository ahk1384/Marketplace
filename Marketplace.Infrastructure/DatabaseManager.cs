using Microsoft.EntityFrameworkCore;
using Marketplace.Domain;

namespace Marketplace.Infrastructure
{
    public class DatabaseManager : DbContext
    {
        public DatabaseManager(DbContextOptions<DatabaseManager> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(255).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(20).IsRequired();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            // Configure Item entity
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("items");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
            });
        }

        public void InitializeDatabase()
        {
            Database.Migrate(); // Use Migrate instead of EnsureCreated for production scenarios
        }
    }
}
