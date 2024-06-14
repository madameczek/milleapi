using System.Data;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Customer> Get(int id, CancellationToken ct)
    {
        var customer= await _dbContext.Customers
            .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false, ct);
        
        if (customer is null)
            throw new RowNotInTableException("Customer not found");
        
        return customer;
    }

    public async Task Update(Customer customer, CancellationToken ct)
    {
        var entity = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id, ct);
        
        if (entity is null)
            throw new RowNotInTableException("Customer not found");

        entity.FirstName = customer.FirstName;
        entity.LastName = customer.LastName;
        entity.IsDeleted = customer.IsDeleted;
        
        _dbContext.Customers.Update(entity);
        await _dbContext.SaveChangesAsync(ct);
    }

    // soft delete
    public async Task Delete(int id, CancellationToken ct)
    {
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id, ct);
     
        if (customer is null)
            throw new RowNotInTableException("Customer not found");

        customer.IsDeleted = true;
        await _dbContext.SaveChangesAsync(ct);
    }
}