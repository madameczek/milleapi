using System.Data;
using milleapi.App.Interfaces;
using milleapi.Entities;

namespace milleapi.App.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Customer> Create(Customer customer, CancellationToken ct)
    {
        return await _repository.Add(customer, ct);
    }

    public Task<Customer> Get(int id, CancellationToken ct)
    {
        if (id % 2 == 0)
            return Task.FromResult(
                new Customer() { FirstName = "Jan", LastName = "Wąski" });
        
        throw new RowNotInTableException();
    }

    public async Task Update(int id, Customer customer, CancellationToken ct)
    {
        await _repository.Update(customer, ct);
    }

    public async Task Remove(int id, CancellationToken ct)
    {
        await _repository.Delete(id, ct);
    }
}