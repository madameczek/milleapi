using milleapi.Entities;

namespace milleapi.App.Interfaces;

public interface ICustomerRepository
{
    public Task<Customer> Add(Customer customer, CancellationToken cancellationToken);
    public Task<Customer> Get(int id, CancellationToken cancellationToken);
    public Task Update(Customer customer,  CancellationToken cancellationToken);
    public Task Delete(int id,  CancellationToken cancellationToken);
}