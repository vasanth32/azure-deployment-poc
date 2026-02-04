# ProductService API

A .NET 8 Web API for managing products with Entity Framework Core and SQL Server.

## Features

- ✅ Entity Framework Core with SQL Server
- ✅ Product entity with Id, Name, Description, Price, Stock
- ✅ RESTful API endpoints (GET, POST)
- ✅ CORS enabled for Angular app
- ✅ Swagger/OpenAPI documentation
- ✅ Health check endpoint
- ✅ Database seeding with 5 sample products
- ✅ Dockerfile for containerization

## Prerequisites

- .NET 8 SDK
- SQL Server (local or Docker)

## Setup

1. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=ProductServiceDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
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
- HTTP: http://localhost:5001
- HTTPS: https://localhost:5002
- Swagger UI: http://localhost:5001/swagger

## API Endpoints

- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create a new product
- `GET /health` - Health check endpoint

## Database

The database is automatically created and seeded with 5 sample products on first run:
- Laptop Computer ($1,299.99)
- Wireless Mouse ($29.99)
- Mechanical Keyboard ($149.99)
- 4K Monitor ($449.99)
- USB-C Hub ($79.99)

## Docker

Build the Docker image:
```bash
docker build -t product-service .
```

Run the container:
```bash
docker run -p 5001:80 -e ConnectionStrings__DefaultConnection="YOUR_CONNECTION_STRING" product-service
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

