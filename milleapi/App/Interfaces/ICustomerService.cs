using milleapi.Entities;

namespace milleapi.App.Interfaces;

public interface ICustomerService
{
    Task<Customer> Create(Customer customer, CancellationToken cancellationToken);
    Task<Customer> Get(int id, CancellationToken cancellationToken);
    Task Update(int id, Customer customer, CancellationToken cancellationToken);
    Task Remove(int id, CancellationToken cancellationToken);
}