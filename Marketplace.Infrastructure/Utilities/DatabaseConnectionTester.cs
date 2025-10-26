using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Marketplace.Infrastructure.Utilities
{
    public static class DatabaseConnectionTester
    {
        public static bool TestConnection(string connectionString)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("? Database connection successful!");
                Console.WriteLine($"Connected to: {connection.Database} on {connection.DataSource}");
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
                var builder = new SqlConnectionStringBuilder(connectionString);
                Console.WriteLine("Connection Details:");
                Console.WriteLine($"Data Source: {builder.DataSource}");
                Console.WriteLine($"Initial Catalog: {builder.InitialCatalog}");
                Console.WriteLine($"Integrated Security: {builder.IntegratedSecurity}");
                Console.WriteLine($"User ID: {(string.IsNullOrEmpty(builder.UserID) ? "Using Windows Authentication" : builder.UserID)}");
                Console.WriteLine($"Password: {(string.IsNullOrEmpty(builder.Password) ? "Not set" : "***")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing connection string: {ex.Message}");
            }
        }

        public static bool TestMultipleServers(string[] serverNames, string database, string? userId = null, string? password = null)
        {
            Console.WriteLine("Testing SQL Server instances...");
            
            foreach (string serverName in serverNames)
            {
                string connectionString;
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    connectionString = $"Data Source={serverName};Initial Catalog={database};User ID={userId};Password={password};Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;";
                }
                else
                {
                    connectionString = $"Data Source={serverName};Initial Catalog={database};Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;";
                }
                
                Console.WriteLine($"Testing server {serverName}...");
                
                try
                {
                    using var connection = new SqlConnection(connectionString);
                    connection.Open();
                    Console.WriteLine($"? SUCCESS! SQL Server found: {serverName}");
                    Console.WriteLine($"Use this connection string: {connectionString}");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"? Server {serverName}: {ex.Message}");
                }
            }
            
            Console.WriteLine("No SQL Server found on tested instances.");
            return false;
        }
    }
}