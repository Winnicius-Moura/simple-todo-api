// Data/AppDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseSqlite("Data Source=app.db;Cache=Shared");

    return new AppDbContext(optionsBuilder.Options);
  }
}

// This factory is used by EF Core tools to create an instance of the DbContext at design time.
// It is typically used for migrations and other design-time operations.