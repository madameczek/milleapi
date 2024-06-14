using milleapi.Models;

namespace milleapi.App.DataSource;

public interface ICustomerService
{
    Task<int> Create(CustomerDto customer, CancellationToken cancellationToken);
    Task<CustomerDto> Get(int id, CancellationToken cancellationToken);
    Task Update(int id, CustomerDto customer, CancellationToken cancellationToken);
    Task Remove(int id, CancellationToken cancellationToken);
}