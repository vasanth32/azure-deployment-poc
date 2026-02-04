# UserService API

A .NET 8 Web API for managing users with Entity Framework Core and SQL Server.

## Features

- ✅ Entity Framework Core with SQL Server
- ✅ User entity with Id, Name, Email, CreatedDate
- ✅ RESTful API endpoints (GET, POST)
- ✅ CORS enabled for Angular app
- ✅ Swagger/OpenAPI documentation
- ✅ Health check endpoint
- ✅ Database seeding with 5 sample users
- ✅ Dockerfile for containerization

## Prerequisites

- .NET 8 SDK
- SQL Server (local or Docker)

## Setup

1. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=UserServiceDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
  }
}
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger UI: http://localhost:5000/swagger

## API Endpoints

- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create a new user
- `GET /health` - Health check endpoint

## Database

The database is automatically created and seeded with 5 sample users on first run.

## Docker

Build the Docker image:
```bash
docker build -t user-service .
```

Run the container:
```bash
docker run -p 5000:80 -e ConnectionStrings__DefaultConnection="YOUR_CONNECTION_STRING" user-service
```

## Migration (Optional)

If you want to use Entity Framework migrations instead of `EnsureCreated()`:

```bash
# Create migration
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

Note: The current implementation uses `EnsureCreated()` which automatically creates the database and tables. For production, consider using migrations.

