using milleapi.Entities;

namespace milleapi.App.Interfaces;

public interface ICustomerRepository
{
    public Task Add(Customer customer, CancellationToken ct);
    public Task<Customer> Get(int id);
    public Task Update(Customer customer);
    public Task Remove(int id);
}