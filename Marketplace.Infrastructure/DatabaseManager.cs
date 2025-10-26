using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Marketplace.Domain;

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

        // Configure relationships / mappings if needed
    }

    public void InitializeDatabase()
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            optionsBuilder.UseMySQL(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
