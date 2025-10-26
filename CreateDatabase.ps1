# Create MarketplaceDb database
$serverName = "AMIRHK"
$databaseName = "MarketplaceDb"

Write-Host "Creating database '$databaseName' on server '$serverName'..." -ForegroundColor Yellow

try {
    # Import SQL Server module if available
    if (Get-Module -ListAvailable -Name SqlServer) {
        Import-Module SqlServer
        
        # Create database using SQL Server PowerShell
        $query = @"
        IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'$databaseName')
        BEGIN
            CREATE DATABASE [$databaseName]
            PRINT 'Database $databaseName created successfully!'
        END
        ELSE
        BEGIN
            PRINT 'Database $databaseName already exists!'
        END
"@
        
        Invoke-Sqlcmd -ServerInstance $serverName -Database "master" -Query $query
        Write-Host "? Database operation completed successfully!" -ForegroundColor Green
    }
    else {
        Write-Host "SQL Server PowerShell module not found. Using SQLCMD..." -ForegroundColor Yellow
        
        # Create SQL script file
        $sqlScript = @"
USE master
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'$databaseName')
BEGIN
    CREATE DATABASE [$databaseName]
    PRINT 'Database $databaseName created successfully!'
END
ELSE
BEGIN
    PRINT 'Database $databaseName already exists!'
END
GO
"@
        
        # Save to temporary file
        $tempFile = Join-Path $env:TEMP "create_db.sql"
        $sqlScript | Out-File -FilePath $tempFile -Encoding UTF8
        
        # Execute using SQLCMD
        sqlcmd -S $serverName -E -i $tempFile
        
        # Clean up
        Remove-Item $tempFile -ErrorAction SilentlyContinue
        
        Write-Host "? Database creation script executed!" -ForegroundColor Green
    }
}
catch {
    Write-Host "? Error creating database: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "Manual steps to create the database:" -ForegroundColor Yellow
    Write-Host "1. Open SQL Server Management Studio (SSMS)" -ForegroundColor White
    Write-Host "2. Connect to server: $serverName" -ForegroundColor White
    Write-Host "3. Right-click on 'Databases' -> 'New Database...'" -ForegroundColor White
    Write-Host "4. Enter database name: $databaseName" -ForegroundColor White
    Write-Host "5. Click 'OK' to create the database" -ForegroundColor White
    Write-Host ""
    Write-Host "Or run this SQL query in SSMS:" -ForegroundColor Yellow
    Write-Host "CREATE DATABASE [$databaseName]" -ForegroundColor Cyan
}

Write-Host ""
Write-Host "After creating the database, stop your application and run:" -ForegroundColor Yellow
Write-Host "dotnet ef database update --project `"Marketplace.Infrastructure`" --startup-project `"Mraketplace.Presention`"" -ForegroundColor Cyan