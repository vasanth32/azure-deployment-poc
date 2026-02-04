# PowerShell script to run Entity Framework migrations
# Usage: .\scripts\migrate-database.ps1 -ConnectionString "YOUR_CONNECTION_STRING"

param(
    [Parameter(Mandatory=$false)]
    [string]$ConnectionString = ""
)

Write-Host "Running Entity Framework migrations..." -ForegroundColor Green

if ([string]::IsNullOrEmpty($ConnectionString))
{
    Write-Host "No connection string provided. Using default from appsettings.json" -ForegroundColor Yellow
    dotnet ef database update
}
else
{
    Write-Host "Using provided connection string" -ForegroundColor Yellow
    $env:ConnectionStrings__DefaultConnection = $ConnectionString
    dotnet ef database update
}

Write-Host "Migration completed!" -ForegroundColor Green

