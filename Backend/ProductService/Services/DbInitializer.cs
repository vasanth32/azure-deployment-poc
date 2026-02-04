using ProductService.Data;
using ProductService.Models;

namespace ProductService.Services;

public class DbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DbInitializer> _logger;

    public DbInitializer(ApplicationDbContext context, ILogger<DbInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void Initialize()
    {
        try
        {
            // Ensure database is created
            _context.Database.EnsureCreated();

            // Check if products already exist
            if (_context.Products.Any())
            {
                _logger.LogInformation("Database already seeded");
                return;
            }

            // Seed initial data
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Laptop Computer",
                    Description = "High-performance laptop with 16GB RAM and 512GB SSD",
                    Price = 1299.99m,
                    Stock = 15
                },
                new Product
                {
                    Name = "Wireless Mouse",
                    Description = "Ergonomic wireless mouse with long battery life",
                    Price = 29.99m,
                    Stock = 50
                },
                new Product
                {
                    Name = "Mechanical Keyboard",
                    Description = "RGB backlit mechanical keyboard with Cherry MX switches",
                    Price = 149.99m,
                    Stock = 25
                },
                new Product
                {
                    Name = "4K Monitor",
                    Description = "27-inch 4K UHD monitor with HDR support",
                    Price = 449.99m,
                    Stock = 10
                },
                new Product
                {
                    Name = "USB-C Hub",
                    Description = "Multi-port USB-C hub with HDMI, USB 3.0, and SD card reader",
                    Price = 79.99m,
                    Stock = 30
                }
            };

            _context.Products.AddRange(products);
            _context.SaveChanges();

            _logger.LogInformation("Database seeded successfully with {Count} products", products.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
}

