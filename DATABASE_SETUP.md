# Marketplace Clean Architecture - Database Configuration

## PostgreSQL Database Setup

This project uses PostgreSQL as the database. Follow these steps to set up your database:

### Prerequisites
1. Install PostgreSQL on your local machine
2. Ensure PostgreSQL service is running on port 5432 (default)

### Database Configuration

#### Connection Strings
- **Development**: `Host=localhost;Port=5432;Database=marketplace_dev_db;Username=postgres;Password=;Include Error Detail=true;`
- **Production**: `Host=localhost;Port=5432;Database=marketplace_db;Username=postgres;Password=;Include Error Detail=true;`

#### Setup Steps

1. **Create Database** (Optional - Entity Framework will create it automatically)
   ```sql
   CREATE DATABASE marketplace_dev_db;
   ```

2. **Run Migrations**
   ```bash
   cd Mraketplace.Presention
   dotnet ef database update --project ..\Marketplace.Infrastructure --startup-project .
   ```

3. **Run the Application**
   ```bash
   dotnet run
   ```

### Database Schema

The application creates the following tables:
- `users` - User information with username, password, age, phone number, email, and balance
- `items` - Marketplace items with name, description, price, creation date, RAM, and storage

### Entity Framework Commands

- **Add Migration**: `dotnet ef migrations add MigrationName --project ..\Marketplace.Infrastructure --startup-project .`
- **Update Database**: `dotnet ef database update --project ..\Marketplace.Infrastructure --startup-project .`
- **Remove Last Migration**: `dotnet ef migrations remove --project ..\Marketplace.Infrastructure --startup-project .`

### Configuration Notes

- The application uses Entity Framework Core with PostgreSQL (Npgsql provider)
- Database initialization happens automatically on application startup
- Migrations are applied if pending during startup
- Table names follow PostgreSQL conventions (lowercase)
- Username field has a unique index to prevent duplicate users

### Environment-Specific Settings

- **Development**: Uses `marketplace_dev_db` database
- **Production**: Uses `marketplace_db` database
- Connection strings can be modified in `appsettings.json` and `appsettings.Development.json`