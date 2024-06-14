using milleapi.App.Interfaces;
using milleapi.App.Persistence.DbContexts;
using milleapi.Entities;

namespace milleapi.App.Persistence;

// consider implementing resilience policies to compensate transient errors
public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _dbContext;

    public CustomerRepository(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Customer> Add(Customer customer, CancellationToken ct)
    {
        var entry = _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync(ct);
        return entry.Entity;
    }

    public Task<Customer> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task Remove(int id)
    {
        throw new NotImplementedException();
    }
}