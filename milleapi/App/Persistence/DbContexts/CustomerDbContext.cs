using Microsoft.EntityFrameworkCore;
using milleapi.Entities;

namespace milleapi.App.Persistence.DbContexts;

public class CustomerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Customer>().HasData(
            new Customer(1, "Kowalski") { FirstName = "Jan", CreatedOn = DateTime.UtcNow },
            new Customer(2, "Adamski") { FirstName = "Adam", CreatedOn = DateTime.UtcNow },
            new Customer(3, "Nowak") { FirstName = "Marcin", CreatedOn = DateTime.UtcNow  }
        );

        base.OnModelCreating(modelBuilder);
    }
}