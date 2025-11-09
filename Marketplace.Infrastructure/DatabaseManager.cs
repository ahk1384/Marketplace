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

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.UserId);
                
                entity.Property(e => e.UserId)
                    .HasColumnName("UserId")
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);
                
                entity.Property(e => e.Age)
                    .IsRequired();
                
                entity.Property(e => e.Balance)
                    .IsRequired()
                    .HasDefaultValue(0);
                
                entity.HasIndex(e => e.Username)
                    .IsUnique();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("items");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);
                
                entity.Property(e => e.Price)
                    .IsRequired();
                
                entity.Property(e => e.CreatedAt)
                    .IsRequired();
                
                entity.Property(e => e.Ram)
                    .IsRequired()
                    .HasDefaultValue(0);
                
                entity.Property(e => e.Storage)
                    .IsRequired()
                    .HasDefaultValue(0);
            });
        }
    }
}
