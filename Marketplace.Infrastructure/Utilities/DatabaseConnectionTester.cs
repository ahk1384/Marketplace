using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Marketplace.Infrastructure.Utilities
{
    public static class DatabaseConnectionTester
    {
        public static bool TestConnection(string connectionString)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("? Database connection successful!");
                Console.WriteLine($"Connected to: {connection.Database} on {connection.Host}:{connection.Port}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("? Database connection failed!");
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public static void PrintConnectionDetails(string connectionString)
        {
            try
            {
                var builder = new NpgsqlConnectionStringBuilder(connectionString);
                Console.WriteLine("Connection Details:");
                Console.WriteLine($"Host: {builder.Host}");
                Console.WriteLine($"Port: {builder.Port}");
                Console.WriteLine($"Database: {builder.Database}");
                Console.WriteLine($"Username: {builder.Username}");
                Console.WriteLine($"Password: {(string.IsNullOrEmpty(builder.Password) ? "Not set" : "***")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing connection string: {ex.Message}");
            }
        }

        public static bool TestMultiplePorts(string host, string username, string password, string database)
        {
            int[] commonPorts = { 5432, 5433, 5434, 5435 };
            
            Console.WriteLine("Testing common PostgreSQL ports...");
            
            foreach (int port in commonPorts)
            {
                var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
                Console.WriteLine($"Testing port {port}...");
                
                try
                {
                    using var connection = new NpgsqlConnection(connectionString);
                    connection.Open();
                    Console.WriteLine($"? SUCCESS! PostgreSQL found on port {port}");
                    Console.WriteLine($"Use this connection string: {connectionString}");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"? Port {port}: {ex.Message}");
                }
            }
            
            Console.WriteLine("No PostgreSQL found on common ports.");
            return false;
        }
    }
}