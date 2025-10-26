# Apply Entity Framework Migrations
Write-Host "Applying Entity Framework migrations..." -ForegroundColor Yellow
Write-Host ""

try {
    # Change to the solution directory
    Set-Location -Path $PSScriptRoot
    
    Write-Host "1. Building solution..." -ForegroundColor Cyan
    dotnet build --configuration Release
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "? Build successful!" -ForegroundColor Green
        
        Write-Host ""
        Write-Host "2. Applying migrations..." -ForegroundColor Cyan
        dotnet ef database update --project "Marketplace.Infrastructure" --startup-project "Mraketplace.Presention" --verbose
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "? Migrations applied successfully!" -ForegroundColor Green
            
            # Test the connection
            Write-Host ""
            Write-Host "3. Testing database connection..." -ForegroundColor Cyan
            dotnet run --project "Mraketplace.Presention" --no-build &
            Start-Sleep -Seconds 3
            
            # Try to make a simple HTTP request to test if the API is working
            try {
                $response = Invoke-WebRequest -Uri "http://localhost:5000" -TimeoutSec 5 -ErrorAction SilentlyContinue
                Write-Host "? API is responding!" -ForegroundColor Green
            } catch {
                Write-Host "? API may be starting up..." -ForegroundColor Yellow
            }
        } else {
            Write-Host "? Migration failed!" -ForegroundColor Red
        }
    } else {
        Write-Host "? Build failed!" -ForegroundColor Red
    }
} catch {
    Write-Host "? Error: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""
Write-Host "If migrations were successful, your database should now have the following tables:" -ForegroundColor Yellow
Write-Host "- users (with columns: UserId, Username, Password, Age, Balance, PhoneNumber, Email)" -ForegroundColor White
Write-Host "- items (with columns: Id, Name, Description, Price, CreatedAt, Ram, Storage)" -ForegroundColor White
Write-Host ""
Write-Host "You can now start your application with:" -ForegroundColor Yellow
Write-Host "dotnet run --project `"Mraketplace.Presention`"" -ForegroundColor Cyan