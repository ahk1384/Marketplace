# Marketplace Database Setup Guide

## Current Status
? SQL Server instance 'AMIRHK' is accessible
? Database 'MarketplaceDb' exists
? Entity Framework migrations are created
? Connection string is configured for SQL Server

## Steps to Fix the Issue

### 1. Stop the Running Application
If your application is currently running, stop it by:
- Pressing `Ctrl+C` in the terminal where it's running
- Or close the terminal/IDE
- Wait for all processes to release the file locks

### 2. Apply Migrations
Run one of these commands from the solution directory:

**Option A: Manual Command**
```bash
dotnet ef database update --project "Marketplace.Infrastructure" --startup-project "Mraketplace.Presention"
```

**Option B: Use the PowerShell Script**
```bash
powershell -ExecutionPolicy Bypass -File "ApplyMigrations.ps1"
```

### 3. Start Your Application
```bash
dotnet run --project "Mraketplace.Presention"
```

## Troubleshooting

### If migrations fail:
1. Check if the application is still running (file locks)
2. Verify SQL Server is running: `services.msc` ? SQL Server services
3. Test connection manually in SQL Server Management Studio

### If you get permission errors:
1. Run PowerShell/Command Prompt as Administrator
2. Or grant your user full permissions to the MarketplaceDb database

### If you want to reset the database:
```sql
-- Run in SQL Server Management Studio
USE master;
DROP DATABASE [MarketplaceDb];
```
Then re-run the migration command.

## Verification
After successful migration, you should see these tables in MarketplaceDb:
- `users` table with columns: UserId, Username, Password, Age, Balance, PhoneNumber, Email
- `items` table with columns: Id, Name, Description, Price, CreatedAt, Ram, Storage
- `__EFMigrationsHistory` table (Entity Framework tracking)

## Connection String Used
```
Data Source=AMIRHK;Initial Catalog=MarketplaceDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False
```