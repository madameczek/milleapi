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
            new Customer { Id = 1, FirstName = "Jan", LastName = "Kowalski", CreatedOn = DateTime.UtcNow },
            new Customer { Id = 2, FirstName = "Adam", LastName = "Adamski", CreatedOn = DateTime.UtcNow },
            new Customer { Id = 3, FirstName = "Marcin", LastName = "Nowak", CreatedOn = DateTime.UtcNow  }
        );

        base.OnModelCreating(modelBuilder);
    }
}