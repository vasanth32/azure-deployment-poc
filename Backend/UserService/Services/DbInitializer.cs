using UserService.Data;
using UserService.Models;

namespace UserService.Services;

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

            // Check if users already exist
            if (_context.Users.Any())
            {
                _logger.LogInformation("Database already seeded");
                return;
            }

            // Seed initial data
            var users = new List<User>
            {
                new User
                {
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-30)
                },
                new User
                {
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-25)
                },
                new User
                {
                    Name = "Bob Johnson",
                    Email = "bob.johnson@example.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-20)
                },
                new User
                {
                    Name = "Alice Williams",
                    Email = "alice.williams@example.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-15)
                },
                new User
                {
                    Name = "Charlie Brown",
                    Email = "charlie.brown@example.com",
                    CreatedDate = DateTime.UtcNow.AddDays(-10)
                }
            };

            _context.Users.AddRange(users);
            _context.SaveChanges();

            _logger.LogInformation("Database seeded successfully with {Count} users", users.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
}

