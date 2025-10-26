using Marketplace.Domain;
using Marketplace.Infrastructure;

namespace Marketplace.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedData(DatabaseManager context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if we already have data
            if (context.Users.Any() || context.Items.Any())
            {
                return; // Database has been seeded
            }

            // Seed Users
            var users = new[]
            {
                new User("admin", "admin123", 30, "+1234567890", "admin@marketplace.com") { Balance = 1000 },
                new User("john_doe", "password123", 25, "+0987654321", "john@example.com") { Balance = 500 },
                new User("jane_smith", "password456", 28, "+1122334455", "jane@example.com") { Balance = 750 }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Seed Items
            var items = new[]
            {
                new Item("Gaming Laptop", "High-performance gaming laptop with RTX 4080", 1500, DateTime.Now.AddDays(-10)) 
                { 
                    Ram = 32, 
                    Storage = 1000 
                },
                new Item("Smartphone", "Latest flagship smartphone with 5G", 800, DateTime.Now.AddDays(-5)) 
                { 
                    Ram = 12, 
                    Storage = 256 
                },
                new Item("Tablet", "10-inch tablet for productivity", 400, DateTime.Now.AddDays(-3)) 
                { 
                    Ram = 8, 
                    Storage = 128 
                },
                new Item("Wireless Headphones", "Premium noise-cancelling headphones", 200, DateTime.Now.AddDays(-1)) 
                { 
                    Ram = 0, 
                    Storage = 0 
                }
            };

            context.Items.AddRange(items);
            context.SaveChanges();
        }
    }
}