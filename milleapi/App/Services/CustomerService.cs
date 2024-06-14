using System.Data;
using milleapi.App.Interfaces;
using milleapi.Entities;
using milleapi.Models;

namespace milleapi.App.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Customer> Create(Customer customer, CancellationToken cancellationToken)
    {
        return await _repository.Add(customer, cancellationToken);
    }

    public Task<Customer> Get(int id, CancellationToken cancellationToken)
    {
        if (id % 2 == 0)
            return Task.FromResult(
                new Customer() { FirstName = "Jan", LastName = "Wąski" });
        
        throw new RowNotInTableException();
    }

    public Task Update(int id, Customer dto, CancellationToken cancellationToken)
    {
        throw new RowNotInTableException();
    }

    public Task Remove(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}